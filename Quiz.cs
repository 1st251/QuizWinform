using QuizWinform.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuizWinform
{
    public partial class Quiz : Form
    {
        private PRN_ProjectContext context;
        public Quiz()
        {
            InitializeComponent();
            context = new PRN_ProjectContext();
        }
        public Quiz(string examCode, string studentName)
        {
            InitializeComponent();

            // Set labels with the values from Form1
            lbExamCode.Text = examCode;
            lbStudent.Text = studentName;
            context = new PRN_ProjectContext();
        }
        private void LoadQuestionById(int questionId)
        {
            try
            {
                // Assuming you have a DbSet property named "Questions" in your DbContext
                var question = context.Questions
                    .Where(q => q.QuestionId == questionId)
                    .FirstOrDefault();

                if (question != null)
                {
                    // Clear existing controls in the group box
                    gbQuestion.Controls.Clear();

                    // Create a Label to display the concatenated string with larger font size and line breaks
                    Label lblQuestion = new Label();
                    lblQuestion.Font = new Font(lblQuestion.Font.FontFamily, 16, FontStyle.Regular); // Set font size to 24
                    lblQuestion.Text = $"Question: {question.QuestionText}\n" +
                                       $"A: {question.OptionA}\n" +
                                       $"B: {question.OptionB}\n" +
                                       $"C: {question.OptionC}\n" +
                                       $"D: {question.OptionD}";
                    lblQuestion.Top = 20;
                    lblQuestion.Width = gbQuestion.Width - 40; // Adjust width based on your design
                    lblQuestion.Height = 200; // Adjust height based on your design
                    lblQuestion.AutoSize = false; // Ensure the label size is fixed

                    // Add the Label to the group box
                    gbQuestion.Controls.Add(lblQuestion);
                }
                else
                {
                    MessageBox.Show("Question not found.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                // Handle exceptions appropriately
                MessageBox.Show("Error loading question: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void Quiz_Load(object sender, EventArgs e)
        {
            LoadQuestionById(1);
        }

        private void label5_Click(object sender, EventArgs e)
        {

        }
    }
}
