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

namespace Displayboth
{
    public partial class Form1 : Form
    {
        
        public Form1()
        {
            InitializeComponent();
        }
        /// <summary>
        /// this function is performed on the load of the form, it gets and outputs the 3 queries to the textbox, 
        /// the first part gets the authors and their isbns and then compares that list with the list of books isbns and outputs which authors are associated with which ibsns
        /// the second part does a very similiar thing except with the book titles ouputted instead of the isbns for easier readability
        /// the last part groups the authors first then outputs each book that they authored using the same method
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Form1_Load(object sender, EventArgs e)
        {
            var dbcontext = new BooksExamples.BooksEntities();

            //

            var authorsAndISBNs =
 from author in dbcontext.Authors
 from book in author.Titles
 orderby author.LastName, author.FirstName
 select new { author.FirstName, author.LastName, book.ISBN };


            outputTextBox.AppendText("Authors and ISBNs:");

            foreach (var element in authorsAndISBNs)
                {
                 outputTextBox.AppendText($"\r\n\t{element.FirstName,-10} " +
                 $"{element.LastName,-10} {element.ISBN,-10}");
                }

            var authorsAndTitles =
 from book in dbcontext.Titles
 from author in book.Authors
 orderby author.LastName, author.FirstName, book.Title1
 select new { author.FirstName, author.LastName, book.Title1 };


            outputTextBox.AppendText("\r\n\r\nAuthors and titles:");


            foreach (var element in authorsAndTitles)
                 {
                 outputTextBox.AppendText($"\r\n\t{element.FirstName,-10} " +
                 $"{element.LastName,-10} {element.Title1}");
                 }

            var titlesByAuthor =
 from author in dbcontext.Authors
 orderby author.LastName, author.FirstName
 select new
 {
     Name = author.FirstName + " " + author.LastName,
     Titles =
 from book in author.Titles
 orderby book.Title1
 select book.Title1
 };

            outputTextBox.AppendText("\r\n\r\nTitles grouped by author:");


            foreach (var author in titlesByAuthor)
            {
                // display author's name
                outputTextBox.AppendText($"\r\n\t{author.Name}:");

                // display titles written by that author
                foreach (var title in author.Titles)
                {
                    outputTextBox.AppendText($"\r\n\t\t{title}");
                }
            }
            }
        }
}
