using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using log4net;

namespace spotchempdf
{
    public partial class frmEditRanges : Form
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(frmEditRanges));
        ReadingRanges rr;

        bool rangeChanged = false;
        bool rangeMaxValid = true;
        bool rangeMinValid = true;
        bool rangeNameValid = true;

        Dictionary<string,Range> availableRanges = new Dictionary<string, Range>();

        string rangesFileName;

        public frmEditRanges(string fname)
        {
            InitializeComponent();
            rangesFileName = fname;
            rr = ReadingRanges.Load(rangesFileName);
            reconnectTypes();
            refreshFields();
        }

        public frmEditRanges(ReadingRanges ra)
        {
            InitializeComponent();
            rr = ra;
            reconnectTypes();
            refreshFields();

        }

        private void reconnectTypes()
        {
            animalType.DataSource = null;
            if (rr.rangeTypes.Count > 0)
            {
                animalType.DataSource = rr.rangeTypes.Keys.ToList();
            }
            else
                log.Debug("No animal types defined");

        }

        private void addAnimalType(string t)
        {
            rr.rangeTypes.Add(t, new RangeType());
            log.Debug("Animal type added " + t);
            forceSaveChangedRange();
        }

        private bool deleteAnimalType(string t)
        {
            bool result = rr.rangeTypes.Remove(t);
            if (!result)
                log.Error("Failed to remove animal type=" + t);
            else
                forceSaveChangedRange();

            return result;
        }


        private void reconnectTypeRanges(string t)
        {
            typeRanges.DataSource = null;
            if (rr.rangeTypes.Count > 0 && rr.rangeTypes.Keys.Contains(t))
            {
                typeRanges.DataSource = rr.rangeTypes[t].ranges.Keys.ToList();
                typeRanges.DisplayMember = "Key";

                log.Debug(rr.rangeTypes[t].ranges.Count + " ranges loaded for animal type="+t+" selected="+typeRanges.SelectedIndex);
            }
            else
                log.Debug("No ranges loaded for animal type="+t);

        }


        private void addType_Click(object sender, EventArgs e)
        {
            if (animalType.Text.Trim().Length == 0)
            {
                MessageBox.Show("Je treba najprv zadať meno typu zvieraťa.");
                return;
            }
            string newType = animalType.Text;
            addAnimalType(newType);
            reconnectTypes();
            int i = animalType.Items.IndexOf(newType);
            animalType.SelectedIndex = i;
            refreshFields();
            refreshRangeFields("", null);
        }

        private void refreshFields()
        {
            log.Debug("Refreshing fields for animal type="+ animalType.Text);
            if (rr.rangeTypes.Keys.Contains(animalType.Text))
            {
                reconnectTypeRanges(animalType.Text);
                addType.Enabled = false;
                deleteType.Enabled = true;
            }
            else
            {
                addType.Enabled = true;
                deleteType.Enabled = false;
            }

        }

        private void typeRanges_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (typeRanges.SelectedIndex < 0)
            {
                deleteRange.Enabled = false;
                refreshRangeFields("", null);
            }
            else
            {
                deleteRange.Enabled = true;
                string name = typeRanges.SelectedItem.ToString();
                rangeName.Text = name;
                Range r = rr.rangeTypes[animalType.Text].ranges[name];
                log.Debug("Going to refresh range fields name=" + name);
                refreshRangeFields(name, r);
            }
        }


        private void refreshRangeFields(string name, Range r)
        {
            log.Debug("Refreshing range fields for range=" + rangeName);
            if (r != null)
            {
                rangeMin.Text = r.min.ToString();
                rangeMax.Text = r.max.ToString();
                rangeUnit.Text = r.unit;
                rangeName.Text = name;

                rangeMax.Enabled = true;
                rangeMin.Enabled = true;
                rangeUnit.Enabled = true;
                rangeName.Enabled = true;
            }
            else
            {
                rangeMax.Enabled = false;
                rangeMin.Enabled = false;
                rangeUnit.Enabled = false;
                rangeName.Enabled = false;

                rangeMin.Text = "";
                rangeMax.Text = "";
                rangeUnit.Text = "";
                rangeName.Text = "";
            }
        }


        private void animalType_TextUpdate(object sender, EventArgs e)
        {
            log.Debug("Animal type changed typed="+animalType.Text);
            refreshFields();
        }

        private void animalType_SelectionChangeCommitted(object sender, EventArgs e)
        {
            log.Debug("Animal type changed selected=" + animalType.SelectedValue);
            if (animalType.SelectedValue != null)
            {
                animalType.Text = animalType.SelectedValue.ToString();
                refreshFields();
            }
        }

        private void deleteType_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Naozaj chcete vymazať typ zvieraťa '" + animalType.Text + "'?", "Potvrdenie", MessageBoxButtons.YesNoCancel) != DialogResult.Yes)
                return;

            if (deleteAnimalType(animalType.Text))
            {
                reconnectTypes();
                animalType.Text = "";
                if (animalType.Items.Count > 0)
                {
                    animalType.SelectedIndex = 0;
                    animalType.Text = animalType.SelectedItem.ToString();
                }
                reconnectTypeRanges(animalType.Text);
                refreshFields();

            }
        }

        private void addRange_Click(object sender, EventArgs e)
        {
            string name = "*new*";
            if (!rr.rangeTypes[animalType.Text].ranges.ContainsKey(name))
            {
                rr.rangeTypes[animalType.Text].ranges.Add(name, new Range());
                reconnectTypeRanges(animalType.Text);
                typeRanges.SetSelected(typeRanges.Items.IndexOf(name),true);
                log.Debug("Selected=" + typeRanges.SelectedItem + " index=" + typeRanges.Items.IndexOf(name));
            } else
            {
                log.Warn("Range exists. name=" + name + " animal type=" + animalType.Text);
            }
        }

        private void deleteRange_Click(object sender, EventArgs e)
        {
            if (typeRanges.SelectedIndex >= 0) {
                string name = typeRanges.SelectedItem.ToString();
                if (MessageBox.Show("Naozaj chcete vymazať rozsah pre '"+name+"'?", "Potvrdenie", MessageBoxButtons.YesNoCancel) != DialogResult.Yes)
                    return;
                log.Debug("Removing range=" + name);
                if (!rr.rangeTypes[animalType.Text].ranges.Remove(name))
                    log.Error("Failed to remove range=" + name);
                else
                {
                    reconnectTypeRanges(animalType.Text);
                    forceSaveChangedRange();
                }
            }
        }


        private void rangeUnit_TextChanged(object sender, EventArgs e)
        {
            if (!rangeUnit.Focused)
                return;

            rr.rangeTypes[animalType.Text].ranges[typeRanges.SelectedItem.ToString()].unit = rangeUnit.Text;
            rangeChanged = true;
            timerSaveRanges.Start();
            log.Debug("Range unit changed");
        }

        private void rangeMax_TextChanged(object sender, EventArgs e)
        {
            if (!rangeMax.Focused)
                return;

            float f = 0;
            rangeChanged = true;
            if (float.TryParse(rangeMax.Text, out f))
            {
                rr.rangeTypes[animalType.Text].ranges[typeRanges.SelectedItem.ToString()].max = f;
                rangeMax.BackColor = SystemColors.Window;
                rangeMaxValid = true;
                timerSaveRanges.Start();
            }
            else
            {
                rangeMax.BackColor = Color.LightSalmon;
                rangeMaxValid = false;
            }
            log.Debug("Range max changed");
        }

        private void rangeMin_TextChanged(object sender, EventArgs e)
        {

            if (!rangeMin.Focused)
                return;

            float f = 0;
            if (float.TryParse(rangeMin.Text, out f))
            {
                rr.rangeTypes[animalType.Text].ranges[typeRanges.SelectedItem.ToString()].min = f;
                rangeMin.BackColor = SystemColors.Window;
                rangeMinValid = true;
                timerSaveRanges.Start();
            }
            else
            {
                rangeMin.BackColor = Color.LightSalmon;
                rangeMinValid = false;
            }

            log.Debug("Range min changed");

        }

        private void rangeName_TextChanged(object sender, EventArgs e)
        {

            if (!rangeName.Focused) return;
            if (typeRanges.SelectedItem.ToString().Equals(rangeName.Text))  return;

            rangeChanged = true;
            
            if (!rr.rangeTypes[animalType.Text].ranges.ContainsKey(rangeName.Text)) {
                rangeNameValid = true;
                rangeName.BackColor = SystemColors.Window;
                timerSaveRanges.Start();
            }
            else
            {
                rangeName.BackColor = Color.LightSalmon;
                rangeNameValid = false;
            }

            log.Debug("Range name changed");
        }

        private void forceSaveChangedRange()
        {
            log.Debug("Forced save");
            rangeChanged = true;
            saveChangedRange();
        }

        private void saveChangedRange()
        {
            bool rangeNameChanged = false;
            string newRangeName = rangeName.Text;
            string oldRangeName = rangeName.Text;

            if (typeRanges.SelectedIndex >= 0)
                oldRangeName = typeRanges.SelectedItem.ToString();

            timerSaveRanges.Stop();

            if (!rangeChanged)
            {
                log.Debug("No changes - not saving");
                return;
            }
            if (!rangeNameValid)
            {
                log.Debug("Invalid range name - not saving");
                return;
            }

            rangeNameChanged = !oldRangeName.Equals(newRangeName);

            // if range name changed then perform change
            if (rangeNameChanged) {
                // get old range values
                Range r = rr.rangeTypes[animalType.Text].ranges[oldRangeName];
                // add range under new name
                rr.rangeTypes[animalType.Text].ranges.Add(newRangeName, r);
                // remove old range
                rr.rangeTypes[animalType.Text].ranges.Remove(oldRangeName);
            }

            bool retry = false;
            do
            {
                try
                {
                    // save ranges
                    rr.Save(rangesFileName);
                    log.Info("Ranges saved to file=" + rangesFileName);

                    rangeChanged = false;
                }
                catch (Exception ex)
                {
                    log.Error("Failed to save range definitions to " + rangesFileName);
                    retry = (MessageBox.Show("Nepodarilo sa uložiť rozsahy testov do súboru "+ Environment.NewLine 
                        + rangesFileName+ Environment.NewLine
                        + Environment.NewLine
                        +"Chyba:"+ex.Message, "", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error) == DialogResult.Retry);
                }

            } while (retry);
            

            if (rangeNameChanged)
            {
                // refresh list of ranges
                reconnectTypeRanges(animalType.Text);
                // reposition selected item
                typeRanges.SetSelected(typeRanges.Items.IndexOf(newRangeName), true);
                //refreshRangeFields();

            }

        }

        private void timerSaveRangeChanges_Tick(object sender, EventArgs e)
        {
            saveChangedRange();
        }


    }
}
