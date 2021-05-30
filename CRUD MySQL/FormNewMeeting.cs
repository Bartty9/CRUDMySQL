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
    public partial class FormNewMeeting : Form
    {
        private readonly FormMeetings _parent;
        public string id, name, place, date, time, participants;
        public FormNewMeeting(FormMeetings parent)
        {
            InitializeComponent();
            _parent = parent;
        }

        public void UpdateInfo()
        {
            lbltext.Text = "Edit Meeting";
            btnSave.Text = "Update";
            txtName.Text = name;
            txtPlace.Text = place;
            datePicker.Value = Convert.ToDateTime(date);
            timePicker.Value = Convert.ToDateTime(time);
        }

        public void SaveInfo()
        {
            lbltext.Text = "New Meeting";
            btnSave.Text = "Save";
        }

        public void Clear()
        {
            txtName.Text = txtPlace.Text = string.Empty;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (txtName.Text.Trim().Length < 3)
            {
                MessageBox.Show("Name is too short ( > 3).");
                return;
            }
            if (txtPlace.Text.Trim().Length < 1)
            {
                MessageBox.Show("Place is Empty ( > 1).");
                return;
            }
            if (btnSave.Text == "Save")
            {
                Meeting meeting = new Meeting(txtName.Text.Trim(), txtPlace.Text.Trim(), datePicker.Value.ToString("dd-MM-yyyy"), timePicker.Value.ToString("HH:mm"));
                DbMeetings.NewMeeting(meeting);
                Clear();
            }
            if (btnSave.Text == "Update")
            {
                Meeting meeting = new Meeting(txtName.Text.Trim(), txtPlace.Text.Trim(), datePicker.Value.ToString("dd-MM-yyyy"), timePicker.Value.ToString("HH:mm"));
                DbMeetings.UpdateMeeting(meeting, id);
            }
            _parent.Display();
        }
    }
}
