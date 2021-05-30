using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CRUD_MySQL
{
    public partial class FormSignUp : Form
    {
        private readonly FormMeetings _parent;
        public string signUpId;
        public FormSignUp(FormMeetings parent)
        {
            InitializeComponent();
            _parent = parent;

        }
        public void SignUpInfo()
        {
            txtParticipant.Text = signUpId;
        }

        public void Clear()
        {
            txtName.Text = txtEmail.Text = string.Empty;
        }


        private void btnSignUp_Click(object sender, EventArgs e)
        {
            if (txtName.Text.Trim().Length < 3)
            {
                MessageBox.Show("Name is too short ( > 3).");
                return;
            }
            if (txtEmail.Text.Trim().Length < 1)
            {
                MessageBox.Show("Email is empty ( > 1).");
                return;
            }
            if (btnSignUp.Text == "Sign Up")
            {
                Participant participant = new Participant(txtName.Text.Trim(), txtEmail.Text.Trim());
                if (DbMeetings.CheckIfTableExists(signUpId))
                {
                    //MessageBox.Show("Table exists.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    if (DbMeetings.CheckNumberOfParticipants(signUpId))
                    {
                        DbMeetings.SignUpParticipant(participant, signUpId);
                        //MessageBox.Show("Participant signed up.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("Can not sign up.\nMaximum number of participants reached!", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                else
                {
                    //MessageBox.Show("Table doesn't exist.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    DbMeetings.CreateParticipantsTable(signUpId);
                    DbMeetings.SignUpParticipant(participant, signUpId);
                }

                Clear();
            }
            _parent.Display();
        }
    }
}
