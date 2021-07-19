﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace KTANE_Solver
{
    //Author: Nya Bentley
    //Date: 3/4/21
    //Purpose: Allows the player to choose which module
    //         they want to be solved
    public partial class ModuleSelectionForm : Form
    {

        //===========FIELDS===========

        //the current bomb
        private Bomb bomb;

        //the confirmation form used get here
        private EdgeworkConfirmationForm confirmationForm;

        //the input form used if the user wants
        //to change the edgework
        private EdgeworkInputForm inputForm;

        //used to write to the log file
        StreamWriter logFileWriter;

        //module forms
        private _3DMazeForm threeDMazeForm;
        private BinaryPuzzleForm binaryForm;
        private CheapCheckoutForm cheapForm;
        private ChessForm chessForm;
        private ComplicatedWiresForm complicatedWiresForm;
        private ColorMathForm colorMathForm;
        private IceCreamForm iceCreamForm;
        private KeypadForm keyPadForm;
        private LogicForm logicForm;
        private MazeForm mazeForm;
        private MurderForm murderForm;
        private NumberPadForm numberPadForm;
        private PasswordFirstStageForm passwordForm;
        private PokerStage1Form pokerForm;
        private RubixCubeForm rubikCubeForm;
        private SillySlotsStage1Form sillySlotsForm;
        private SimonSaysForm simonSaysForm;
        private TwoBitsStage1Form twoBitsForm;
        private WhosOnFirstFirstStageForm whosOnFirstForm;
        private WiresForm wiresForm;
        private WordSearchForm wordSearchForm;


        //===========CONSTRUCTORS===========

        

        /// <summary>
        /// Creates an form that allows the user to
        /// pick which module wants to be solved
        /// </summary>
        /// <param name="bomb">the current bomb</param>
        /// <param name="confirmationForm">the form used if the player wants to check the edgework</param>
        /// <param name="inputForm">the form used if the player want to change the edgework</param>
        public ModuleSelectionForm(Bomb bomb, EdgeworkConfirmationForm confirmationForm, EdgeworkInputForm inputForm, StreamWriter logFileWriter)
        {
            InitializeComponent();
            this.logFileWriter = logFileWriter;
            this.bomb = bomb;
            this.confirmationForm = confirmationForm;
            this.inputForm = inputForm;
            UpdateForm();
        }


        //===========METHODS===========

        /// <summary>
        /// Sets up the form, so it looks
        /// good as new
        /// </summary>
        public void UpdateForm()
        {
            logFileWriter.WriteLine("======================MODULE SELECTION======================");
            SetUpModuleComboBox();
        }

        /// <summary>
        /// Sets up the form, so it looks
        /// good as new
        /// </summary>
        /// <param name="bomb">the new bomb</param>
        public void UpdateForm(Bomb bomb)
        {
            logFileWriter.WriteLine("======================MODULE SELECTION======================");
            SetUpModuleComboBox();
            this.bomb = bomb;
        }

        /// <summary>
        /// Sets up the combo box so it has all the modules 
        /// </summary>
        private void SetUpModuleComboBox()
        {
            moduleComboBox.Items.Clear();

            String[] modules = new String[] {"3D Maze", "Binary Puzzle", "Cheap Checkout", "Chess", "Color Math", "Complicated Wires", "Ice Cream", "Keypad", "Logic", "Maze", "Murder","Number Pad", "Password", "Poker", "Rubik's Cube", "Silly Slots", "Simon Says", "Two Bits", "Who's on First", "Wires", "Word Search" };

            moduleComboBox.Items.AddRange(modules);
            moduleComboBox.Text = modules[0];
            moduleComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
        }

        /// <summary>
        /// Sends the user to the edgework input form
        /// </summary>
        private void changeEdgeworkButton_Click(object sender, EventArgs e)
        {
            logFileWriter.WriteLine("User is changing edgework...\n");
            this.Hide();
            inputForm.UpdateForm();
            inputForm.Show();
        }

        /// <summary>
        /// sends the user to the edgework confirmation form
        /// </summary>
        private void checkEdgeworkButton_Click(object sender, EventArgs e)
        {
            logFileWriter.WriteLine("User is checking edgework...\n");
            this.Hide();
            confirmationForm.UpdateForm(bomb, inputForm);
            confirmationForm.Show();
        }

        /// <summary>
        /// Confirms that the user want to close the program
        /// </summary>
        private void ModuleSelectionForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (this.Visible)
            {
                String message = "Are you sure you want to quit the program?";
                String caption = "Quit Program";

                DialogResult result = MessageBox.Show(message, caption, MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                //if the user clicks no, don't close the program
                if (result == DialogResult.No)
                {
                    e.Cancel = true;
                }

                else
                {
                    this.Visible = false;
                    Application.Exit();
                }
            }
        }

        /// <summary>
        /// Saves the current bomb to Edgework.txt
        /// </summary>
        private void saveEdgeworkButton_Click(object sender, EventArgs e)
        {
            logFileWriter.WriteLine("User is trying to save edgework...\n");

            StreamWriter writer = new StreamWriter("Edgework.txt");

            try
            {
                writer.WriteLine(bomb.Day);
                writer.WriteLine(bomb.SerialNumber);
                writer.WriteLine(bomb.Battery);
                writer.WriteLine(bomb.BatteryHolder);

                SaveIndicator(writer, bomb.Bob);
                SaveIndicator(writer, bomb.Car);
                SaveIndicator(writer, bomb.Clr);
                SaveIndicator(writer, bomb.Frk);
                SaveIndicator(writer, bomb.Frq);
                SaveIndicator(writer, bomb.Ind);
                SaveIndicator(writer, bomb.Msa);
                SaveIndicator(writer, bomb.Nsa);
                SaveIndicator(writer, bomb.Sig);
                SaveIndicator(writer, bomb.Snd);
                SaveIndicator(writer, bomb.Trn);

                writer.WriteLine(bomb.EmptyPortPlate);
                writer.WriteLine(bomb.Dvid.Num);
                writer.WriteLine(bomb.Parallel.Num);
                writer.WriteLine(bomb.Ps.Num);
                writer.WriteLine(bomb.Rj.Num);
                writer.WriteLine(bomb.Serial.Num);
                writer.WriteLine(bomb.Stereo.Num);

                writer.Close();

                MessageBox.Show("Edgework saved successfully", "Edgework Saved", MessageBoxButtons.OK, MessageBoxIcon.Information);
                logFileWriter.WriteLine("User has successfully saved edgeork\n");
            }

            catch
            {
                MessageBox.Show("There was an error saving this edgework to Edgework.txt", "Saving Error",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Error);

                logFileWriter.WriteLine("User has unsuccessfully saved edgeork\n");
            }
        }

        /// <summary>
        /// Saves the indicators to Edgework.txt
        /// </summary>
        /// <param name="writer">used to save the data<param>
        /// <param name="indicator">the indicator that will be saved</param>
        private void SaveIndicator(StreamWriter writer, Indicator indicator)
        {
            writer.WriteLine($"{indicator.Visible}|{indicator.Lit}");
        }

        
        /// <summary>
        /// Sends the player to the module form
        /// they selected in the combo box
        /// </summary>
        private void submitButton_Click(object sender, EventArgs e)
        {
            String module = moduleComboBox.Text;

            logFileWriter.WriteLine($"User selected {module}. Attempting to open...\n");

            this.Hide();

            switch (module)
            {
                case "3D Maze":
                    

                    if (threeDMazeForm == null)
                    {
                        threeDMazeForm = new _3DMazeForm(bomb, logFileWriter, this);
                    }

                    else
                    {
                        threeDMazeForm.UpdateForm(bomb, logFileWriter, this);
                    }

                    threeDMazeForm.Show();

                    break;
                case "Binary Puzzle":

                    if (binaryForm == null)
                    {
                        binaryForm = new BinaryPuzzleForm(this, logFileWriter);
                    }

                    else
                    {
                        binaryForm.UpdateForm(this, logFileWriter);
                    }

                    binaryForm.Show();
                    break;

                case "Cheap Checkout":
   
                    if (cheapForm == null)
                    {
                        cheapForm = new CheapCheckoutForm(bomb, logFileWriter, this);
                    }

                    else
                    {
                        cheapForm.UpdateForm(bomb, logFileWriter, this);
                    }

                    cheapForm.Show();
                    break;

                case "Chess":
                    
                    if (chessForm == null)
                    {
                        chessForm = new ChessForm(bomb, logFileWriter, this);
                    }

                    else
                    {
                        chessForm.UpdateForm(bomb, logFileWriter, this);
                    }

                    chessForm.Show();
                    break;

                case "Color Math":

                    if (colorMathForm == null)
                    {
                        colorMathForm = new ColorMathForm(bomb, logFileWriter, this);
                    }
                    else
                    {
                        colorMathForm.UpdateForm(bomb, logFileWriter, this);
                    }

                    colorMathForm.Show();
                    break;

                case "Complicated Wires":

                    if (complicatedWiresForm == null)
                    {
                        complicatedWiresForm = new ComplicatedWiresForm(bomb, logFileWriter, this);
                    }
                    else
                    {
                        complicatedWiresForm.UpdateForm(bomb, logFileWriter, this);
                    }

                    complicatedWiresForm.Show();
                    break;

                case "Ice Cream":

                    if (iceCreamForm == null)
                    {
                        iceCreamForm = new IceCreamForm(bomb, logFileWriter, this);
                    }

                    else
                    {
                        iceCreamForm.UpdateForm(bomb, logFileWriter, this, 1);
                    }

                    iceCreamForm.Show();
                    break;

                case "Keypad":

                    if (keyPadForm == null)
                    {
                        keyPadForm = new KeypadForm(bomb, logFileWriter, this);
                    }

                    else
                    {
                        keyPadForm.UpdateForm(bomb, logFileWriter, this);
                    }

                    keyPadForm.Show();
                    break;

                case "Logic":

                    if (logicForm == null)
                    {
                        logicForm = new LogicForm(bomb, logFileWriter, this);
                    }

                    else
                    {
                        logicForm.UpdateForm(bomb, logFileWriter, this);
                    }

                    logicForm.Show();
                    break;

                case "Maze":

                    if (mazeForm == null)
                    {
                        mazeForm = new MazeForm(bomb, logFileWriter, this);
                    }

                    else
                    {
                        mazeForm.UpdateForm(bomb, logFileWriter, this);
                    }

                    mazeForm.Show();
                    break;

                case "Number Pad":

                    if (numberPadForm == null)
                    {
                        numberPadForm = new NumberPadForm(bomb, logFileWriter, this);
                    }

                    else
                    {
                        numberPadForm.UpdateForm(bomb, logFileWriter, this);
                    }

                    numberPadForm.Show();
                    break;

                case "Password":

                    if (passwordForm == null)
                    {
                        passwordForm = new PasswordFirstStageForm(bomb, logFileWriter, this);
                    }

                    else
                    {
                        passwordForm.UpdateForm(bomb, logFileWriter, this);
                    }

                    passwordForm.Show();
                    break;


                case "Poker":

                    if (pokerForm == null)
                    {
                        pokerForm = new PokerStage1Form(bomb, logFileWriter, this);
                    }

                    else
                    {
                        pokerForm.UpdateForm(bomb, logFileWriter, this);
                    }

                    pokerForm.Show();
                    break;

                case "Rubik's Cube":

                    if (rubikCubeForm == null)
                    {
                        rubikCubeForm = new RubixCubeForm(bomb, logFileWriter, this);
                    }

                    else
                    {
                        rubikCubeForm.UpdateForm(bomb, logFileWriter, this);
                    }

                    rubikCubeForm.Show();
                    break;

                case "Silly Slots":

                    if (sillySlotsForm == null)
                    {
                        sillySlotsForm = new SillySlotsStage1Form(bomb, logFileWriter, this);
                    }

                    else
                    {
                        sillySlotsForm.UpdateForm(bomb, logFileWriter, this);
                    }

                    sillySlotsForm.Show();
                    break;

                case "Simon Says":

                    if (simonSaysForm == null)
                    {
                        simonSaysForm = new SimonSaysForm(bomb, logFileWriter, this);
                    }

                    else
                    {
                        simonSaysForm.UpdateForm(bomb, logFileWriter, this);
                    }

                    simonSaysForm.Show();
                    break;

                case "Murder":

                    if (murderForm == null)
                    {
                        murderForm = new MurderForm(bomb, logFileWriter, this);
                    }

                    else
                    {
                        murderForm.UpdateForm(bomb, logFileWriter, this);
                    }

                    murderForm.Show();
                    break;

                case "Who's on First":

                    if (whosOnFirstForm == null)
                        whosOnFirstForm = new WhosOnFirstFirstStageForm(bomb, logFileWriter, this);

                    else
                        whosOnFirstForm.UpdateForm(bomb, logFileWriter, this);

                    whosOnFirstForm.Show();
                    break;

                case "Two Bits":

                    if (twoBitsForm == null)
                    {
                        twoBitsForm = new TwoBitsStage1Form(bomb, logFileWriter, this);
                    }

                    else
                    {
                        twoBitsForm.UpdateForm(bomb, logFileWriter, this);
                    }

                    twoBitsForm.Show();
                    break;

                case "Wires":

                    if (wiresForm == null)
                    {
                        wiresForm = new WiresForm(bomb, logFileWriter, this);
                    }

                    else
                    {
                        wiresForm.UpdateForm(bomb, logFileWriter, this);
                    }

                    wiresForm.Show();
                    break;

                case "Word Search":

                    if (wordSearchForm == null)
                    {
                        wordSearchForm = new WordSearchForm(bomb, logFileWriter, this);
                    }

                    else
                    {
                        wordSearchForm.UpdateForm(bomb, logFileWriter, this);
                    }

                    wordSearchForm.Show();
                    break;

                //this means that the module doesn't exist
                default:
                    this.Show();
                break;
            }

            SuccessfulModuleOpening(module);
        }


        /// <summary>
        /// Tells the log the module opend successfully
        /// </summary>
        /// <param name="module"></param>
        private void SuccessfulModuleOpening(String module)
        {
            logFileWriter.WriteLine($"{module} opened successfully\n");
        }
    }
}
