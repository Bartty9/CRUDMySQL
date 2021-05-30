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
    public partial class FormMeetings : Form
    {
        FormNewMeeting form;
        public FormMeetings()
        {
            InitializeComponent();
            form = new FormNewMeeting(this);
        }

        public void Display()
        {
            DbMeetings.DisplaySearch("SELECT ID, Name, Place, Date, Time FROM meetings_table", dataGridView);
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            form.Clear();
            form.SaveInfo();
            form.ShowDialog();
        }
        private void FormMeetings_Shown(object sender, EventArgs e)
        {
            Display();
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            DbMeetings.DisplaySearch("SELECT ID, Name, Place, Date, Time FROM meetings_table WHERE Name LIKE'%" + txtSearch.Text + "%'", dataGridView);
        }

        private void dataGridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 0) //SHOW PARTICIPANTS
            {
                FormShowParticipants formShowParticipants = new FormShowParticipants(this);
                formShowParticipants.showId = dataGridView.Rows[e.RowIndex].Cells[4].Value.ToString();
                if (DbMeetings.CheckIfTableExists(formShowParticipants.showId))
                {
                    formShowParticipants.ShowParticipants();
                    formShowParticipants.ShowDialog();
                    return;
                }
                else
                {
                    MessageBox.Show("There are no participants signed up for this event.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }  
            }            
            if (e.ColumnIndex == 1) //SIGN UP
            {
                FormSignUp formSignUp = new FormSignUp(this);
                formSignUp.signUpId = dataGridView.Rows[e.RowIndex].Cells[4].Value.ToString();
                formSignUp.SignUpInfo();
                formSignUp.ShowDialog();
                return;
            }
            if (e.ColumnIndex == 2) //EDIT
            {                
                form.Clear();
                form.id = dataGridView.Rows[e.RowIndex].Cells[4].Value.ToString();
                form.name = dataGridView.Rows[e.RowIndex].Cells[5].Value.ToString();
                form.place = dataGridView.Rows[e.RowIndex].Cells[6].Value.ToString();
                form.date = dataGridView.Rows[e.RowIndex].Cells[7].Value.ToString();
                form.time = dataGridView.Rows[e.RowIndex].Cells[8].Value.ToString();
                form.UpdateInfo();
                form.ShowDialog();
                return;
            }
            if (e.ColumnIndex == 3) //DELETE
            {
                if (MessageBox.Show("Do you want to delete meeting?", "Information", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Information) == DialogResult.Yes)
                {
                    DbMeetings.DeleteMeeting(dataGridView.Rows[e.RowIndex].Cells[4].Value.ToString());
                    Display();
                }
                return;
            }
        }
    }
}
