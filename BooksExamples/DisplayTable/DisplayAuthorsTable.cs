using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DisplayTable
{
    public partial class DisplayAuthorsTable : Form
    {
        public DisplayAuthorsTable()
        {
            InitializeComponent();
        }

        bool check = true;
        //Entity Framework DbContext
        private BooksExamples.BooksEntities dbcontext = new BooksExamples.BooksEntities();
        //load data from database into DataGridView
        /// <summary>
        /// this function orders the authors on load of the form by last name then first name
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DisplayAuthorsTable_Load(object sender, EventArgs e)
        {
            //load Authors table ordered by LastName then FirstName
            dbcontext.Authors
                .OrderBy(author => author.LastName)
                .ThenBy(author => author.FirstName)
                .Load();
            //specify datasource for authorBindingSource
            authorBindingSource.DataSource = dbcontext.Authors.Local;

        }

        /// <summary>
        /// this function has a bool with it so that way it will only display the original list once and not constantly by last name then first name
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void authorBindingNavigator_RefreshItems(object sender, EventArgs e)
        {
            if(check)
            {
                //load Authors table ordered by LastName then FirstName
                dbcontext.Authors
                    .OrderBy(author => author.LastName)
                    .ThenBy(author => author.FirstName)
                    .Load();
                //specify datasource for authorBindingSource
                authorBindingSource.DataSource = dbcontext.Authors.Local;
                check = false;
            }
        }

        /// <summary>
        /// this funcction saves the changes made to the database in the form
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void authorBindingNavigatorSaveItem_Click(object sender, EventArgs e)
        {
            Validate();
            authorBindingSource.EndEdit();
            try
            {
                dbcontext.SaveChanges();
            }
            catch (System.Data.Entity.Validation.DbEntityValidationException)
            {
                MessageBox.Show("FirstName and LastName must contain values", "Entity Validation Exception");
            }
        }
        /// <summary>
        /// this function takes the text from the textbox when clicked and searches through the author names and then makes a list of all authors that start with the text
        /// then the list is outputted to the form ordered by first name
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void findButton_Click(object sender, EventArgs e)
        {

            //load Authors table ordered by LastName then FirstName
            dbcontext.Authors
                .OrderBy(author => author.FirstName)
                .ThenBy(author => author.LastName)
                .Load();
            //specify datasource for authorBindingSource
            authorBindingSource.DataSource = dbcontext.Authors.Local;



            var lastNameQuery =
 from author in dbcontext.Authors
 where author.LastName.StartsWith(findTextBox.Text)
 orderby author.LastName, author.FirstName
 select author;

            authorBindingSource.DataSource = lastNameQuery.ToList();
            authorBindingSource.MoveFirst();
        }
        /// <summary>
        /// this function is used when you want to revert back to the orignal sorted list after looking for a specific author name
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void browseAllButton_Click(object sender, EventArgs e)
        {
            //load Authors table ordered by LastName then FirstName
            dbcontext.Authors
                .OrderBy(author => author.LastName)
                .ThenBy(author => author.FirstName)
                .Load();
            //specify datasource for authorBindingSource
            authorBindingSource.DataSource = dbcontext.Authors.Local;
        }
    }
}
