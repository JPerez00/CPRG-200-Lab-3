using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

/* Lab 3
 * Create a Windows Forms application that connects to the Northwind database using data source that
 * includes tables: Products and Order Details.
 * Dev: Jorge Perez
 * Date: Jan 21, 2020
 */

namespace Lab3
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void productsBindingNavigatorSaveItem_Click(object sender, EventArgs e)
        {
            try
            {
                this.Validate();
                this.productsBindingSource.EndEdit();
                this.tableAdapterManager.UpdateAll(this.northwindDataSet);
            }
            catch (DBConcurrencyException)
            {
                MessageBox.Show("Another user updated or deleted customer's data in the meantime. " +
                    "Try again.", "Conflict with concurrent user");
                this.productsTableAdapter.Fill(this.northwindDataSet.Products); // reload table
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error while saving data: " + ex.Message,
                    ex.GetType().ToString());
            }
            //catch (SqlException ex)
            //{
            //    MessageBox.Show("Database error # " + ex.Number + ": " + ex.Message, ex.GetType().ToString());
            //}
        }

        private void order_DetailsDataGridView_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            int row = e.RowIndex + 1;
            int col = e.ColumnIndex + 1;
            MessageBox.Show("Data error in the grid: row " + row + " and column " + col, e.Exception.GetType().ToString());
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            try
            {
                // Loads data into the 'northwindDataSet.Order_Details' table. 
                this.order_DetailsTableAdapter.Fill(this.northwindDataSet.Order_Details);
                // Loads data into the 'northwindDataSet.Categories' table.
                this.categoriesTableAdapter.Fill(this.northwindDataSet.Categories);
                // Loads data into the 'northwindDataSet.Suppliers' table.
                this.suppliersTableAdapter.Fill(this.northwindDataSet.Suppliers);
                // Loads data into the 'northwindDataSet.Products' table.
                this.productsTableAdapter.Fill(this.northwindDataSet.Products);
            }
            catch (Exception ex)
            { 
                MessageBox.Show("Error while loading data: " + ex.Message,
                    ex.GetType().ToString());
            }
        }
    }
}
