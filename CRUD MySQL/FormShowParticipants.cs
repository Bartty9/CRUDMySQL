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
    public partial class FormShowParticipants : Form
    {
        private readonly FormMeetings _parent;
        public string showId;
        public FormShowParticipants(FormMeetings parent)
        {
            InitializeComponent();
            _parent = parent;           
        }
        public void ShowParticipants()
        {
            DbMeetings.DisplaySearch("Select ParticipantID, Participant FROM " + showId + "table", dataGridViewShow);
        }
    }
}
