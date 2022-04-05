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

namespace DisplayTitles
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        private BooksExamples.BooksEntities dbcontext = new BooksExamples.BooksEntities();
        /// <summary>
        /// this function orders the titles by isbn on load of the form
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Form1_Load(object sender, EventArgs e)
        {
            //load Authors table ordered by LastName then FirstName
            dbcontext.Titles
                .OrderBy(titles => titles.ISBN)
                .Load();
            //specify datasource for authorBindingSource
            titleBindingSource.DataSource = dbcontext.Titles.Local;
        }
        /// <summary>
        /// this button takes input from the textbox and then compares that to the database and makes a list of all the book titles that contain that string somewhere
        /// in the title, it is caps lock sensative and then outputs them to the form
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void containsButton_Click(object sender, EventArgs e)
        {
            titleBindingSource.DataSource =
 dbcontext.Titles.Local
 .Where(book => book.Title1.Contains(containsTextBox.Text))
 .OrderBy(book => book.Title1);
        }
        /// <summary>
        /// this function outputs the list of all titles sorted by isbn after youve searched for some string of letters in the titles
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void restartButton_Click(object sender, EventArgs e)
        {
            


            //load Authors table ordered by LastName then FirstName
            dbcontext.Titles
                .OrderBy(titles => titles.ISBN)
                .Load();
            //specify datasource for authorBindingSource
            titleBindingSource.DataSource = dbcontext.Titles.Local;
        }
    }
}
