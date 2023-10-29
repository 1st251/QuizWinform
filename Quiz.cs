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
        private List<Question> questions; // Add this variable to store the loaded questions
        private int currentQuestionIndex = 0; // Add this variable to keep track of the current question index
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
        private void LoadQuestionByExamCode(string examCode)
        {
            try
            {
                // Assuming you have a DbSet property named "Exams" in your DbContext
                var exam = context.Exams
                    .Where(e => e.ExamName == examCode)
                    .FirstOrDefault();

                if (exam != null)
                {
                    // Get the ExamId for the specified ExamCode
                    int examId = exam.ExamId;

                    // Assuming you have a DbSet property named "Questions" in your DbContext
                    var question = context.Questions
                        .Where(q => q.ExamId == examId)
                        .FirstOrDefault();

                    if (question != null)
                    {
                        // Clear existing controls in the group box
                        gbQuestion.Controls.Clear();

                        // Create a Label to display the concatenated string with larger font size and line breaks
                        Label lblQuestion = new Label();
                        lblQuestion.Font = new Font(lblQuestion.Font.FontFamily, 16, FontStyle.Regular); // Set font size to 16
                        lblQuestion.Text = $"Question 1: {question.QuestionText}\n" +
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
                        MessageBox.Show("No questions found for the specified ExamCode.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                else
                {
                    MessageBox.Show("Exam not found for the specified ExamCode.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                // Handle exceptions appropriately
                MessageBox.Show("Error loading question: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CreateQuestionButtonsForExam(int examId)
        {
            try
            {
                // Assuming you have a DbSet property named "Questions" in your DbContext
                questions = context.Questions
                    .Where(q => q.ExamId == examId)
                    .ToList();

                if (questions.Any())
                {
                    // Clear existing controls outside the group box
                    ClearButtons();

                    int buttonTop = 660;
                    int buttonLeft = 152;
                    int buttonWidth = 28;
                    int buttonHeight = 27;
                    int horizontalSpacing = 15;
                    int buttonNumber = 1;

                    foreach (var question in questions)
                    {
                        int currentButtonNumber = buttonNumber; // Capture the current button number in a local variable

                        // Create a button for each question
                        Button btnQuest = new Button();
                        btnQuest.Location = new Point(buttonLeft, buttonTop);
                        btnQuest.Size = new Size(buttonWidth, buttonHeight);
                        btnQuest.Text = currentButtonNumber.ToString();
                        btnQuest.Name = $"btnQuest{question.QuestionId}";
                        btnQuest.Click += (sender, e) =>
                        {
                            // Load the corresponding question without using LoadQuestionById
                            var selectedQuestion = context.Questions.FirstOrDefault(q => q.QuestionId == question.QuestionId);

                            if (selectedQuestion != null)
                            {
                                // Clear existing controls in the group box
                                gbQuestion.Controls.Clear();

                                // Create a Label to display the concatenated string with larger font size and line breaks
                                Label lblQuestion = new Label();
                                lblQuestion.Font = new Font(lblQuestion.Font.FontFamily, 16, FontStyle.Regular); // Set font size to 16
                                lblQuestion.Text = $"Question {currentButtonNumber.ToString()}: {selectedQuestion.QuestionText}\n" +
                                                   $"A: {selectedQuestion.OptionA}\n" +
                                                   $"B: {selectedQuestion.OptionB}\n" +
                                                   $"C: {selectedQuestion.OptionC}\n" +
                                                   $"D: {selectedQuestion.OptionD}";
                                lblQuestion.Top = 20;
                                lblQuestion.Width = gbQuestion.Width - 40; // Adjust width based on your design
                                lblQuestion.Height = 200; // Adjust height based on your design
                                lblQuestion.AutoSize = false; // Ensure the label size is fixed

                                // Add the Label to the group box
                                gbQuestion.Controls.Add(lblQuestion);

                                // Update the current question index
                                currentQuestionIndex = currentButtonNumber - 1;

                                // Update the lbQuestion label
                                UpdateQuestionLabel();
                            }
                            else
                            {
                                MessageBox.Show("Question not found.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                        };

                        Controls.Add(btnQuest);

                        // Adjust button positions
                        buttonLeft += buttonWidth + horizontalSpacing; // Adjust the spacing between buttons
                        buttonNumber++; // Increment the button number
                    }

                    // Update the lbQuestion label after creating buttons
                    UpdateQuestionLabel();
                }
                else
                {
                    MessageBox.Show("No questions found for the specified ExamId.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                // Handle exceptions appropriately
                MessageBox.Show("Error loading questions: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void LoadQuestion(Question question, int currentButtonNumber)
        {
            // Load the specified question
            if (question != null)
            {
                // Clear existing controls in the group box
                gbQuestion.Controls.Clear();

                // Create a Label to display the concatenated string with larger font size and line breaks
                Label lblQuestion = new Label();
                lblQuestion.Font = new Font(lblQuestion.Font.FontFamily, 16, FontStyle.Regular); // Set font size to 16
                lblQuestion.Text = $"Question {currentButtonNumber+1}: {question.QuestionText}\n" +
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

        private void ClearButtons()
        {
            foreach (Control control in Controls)
            {
                if (control is Button && control.Name.StartsWith("btnQuest"))
                {
                    Controls.Remove(control);
                }
            }
        }

        private void Quiz_Load(object sender, EventArgs e)
        {
            var examCode = lbExamCode.Text;

            // Assuming you have a DbSet property named "Exams" in your DbContext
            var exam = context.Exams
                .Where(e => e.ExamName == examCode)
                .FirstOrDefault();
            LoadQuestionByExamCode(examCode);
            if (exam != null)
            {
                // Get the ExamId for the specified ExamCode
                int examId = exam.ExamId;

                // Create buttons for each question in the exam
                CreateQuestionButtonsForExam(examId);
            }
            else
            {
                MessageBox.Show("Exam not found for the specified ExamCode.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            // Navigate to the next question
            if (questions != null && currentQuestionIndex < questions.Count - 1)
            {
                currentQuestionIndex++;
            }
            else if (questions != null && questions.Any())
            {
                // If we reached the last question, go back to the first question
                currentQuestionIndex = 0;
            }
            else
            {
                MessageBox.Show("No questions found.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return; // Return here to prevent further execution if there are no questions
            }

            // Load the question based on the updated index
            LoadQuestion(questions[currentQuestionIndex], currentQuestionIndex);

            // Update the lbQuestion label
            UpdateQuestionLabel();
        }

        private void cbWarning_CheckedChanged(object sender, EventArgs e)
        {
            btnFinish.Enabled = true;
            if (cbWarning.Checked == false)
            {
                btnFinish.Enabled = false;
            }
        }
        private void UpdateQuestionLabel()
        {
            lbQuestion.Text = $"{currentQuestionIndex + 1}/{questions.Count}";
        }
    }
}
