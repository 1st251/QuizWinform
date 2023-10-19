using QuizWinform.Models;

namespace QuizWinform
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string examName = txtExam.Text;
            string username = txtUsername.Text;
            string password = txtPassword.Text;

            // Initialize your DbContext instance
            using (var context = new PRN_ProjectContext()) // Replace YourDbContext with your actual DbContext class name
            {
                // Check if the exam name exists in the Exams table
                var exam = context.Exams.SingleOrDefault(e => e.ExamName == examName);

                // Check if the username and password match a student record
                var student = context.Students.SingleOrDefault(s => s.Username == username);

                // Check if both exam and student records exist
                if (exam != null)
                {
                    if (student != null && student.Password == password)
                    {
                        // Optionally, you can check if the student is associated with the selected exam
                        // Exam and student are valid
                        MessageBox.Show("Login successful!");
                    }
                    else if (student == null)
                    {
                        // Invalid username
                        MessageBox.Show("Invalid Username.");
                    }
                    else
                    {
                        // Invalid password
                        MessageBox.Show("Invalid Password.");
                    }
                }
                else
                {
                    // Invalid exam code
                    MessageBox.Show("Invalid Exam Code.");
                }
            }

        }
    }
}