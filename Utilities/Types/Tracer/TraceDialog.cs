using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Utilities.Types.Tracer
{
    internal partial class TraceDialog : Form
    {
        internal Tracing.TraceDecision Decision { get; private set; }
        Point? _previousDialogLocation = null;

        public TraceDialog()
        {
            InitializeComponent();
        }

        public void Initialize(string @object, string member, string line, string commentary)
        {
            lbl_Object.Text = @object;
            lbl_Member.Text = member;
            lbl_Line.Text = line;
            lbl_Commentary.Text = commentary;
        }

        protected override void OnShown(EventArgs e)
        {
            Location = _previousDialogLocation ?? Location;

            base.OnShown(e);
        }

        private void btn_Next_Click(object sender, EventArgs e)
        {
            makeDecisionAndHide(Tracing.TraceDecision.NEXT);
        }

        private void btn_IgnoreObject_Click(object sender, EventArgs e)
        {
            makeDecisionAndHide(Tracing.TraceDecision.IGNORE_OBJECT);
        }

        private void btn_IgnoreMember_Click(object sender, EventArgs e)
        {
            makeDecisionAndHide(Tracing.TraceDecision.IGNORE_MEMBER);
        }

        private void btn_IgnoreLine_Click(object sender, EventArgs e)
        {
            makeDecisionAndHide(Tracing.TraceDecision.IGNORE_LINE);
        }

        private void btn_CompleteTracing_Click(object sender, EventArgs e)
        {
            makeDecisionAndHide(Tracing.TraceDecision.COMPLETE_TRACING);
        }

        void makeDecisionAndHide(Tracing.TraceDecision decision)
        {
            _previousDialogLocation = Location;
            Decision = decision;
            Hide();
        }
    }
}
