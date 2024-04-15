using System.Drawing;
using System.Drawing.Imaging;
using System.Windows.Forms;

namespace FHP
{
    public partial class AboutUs : Form
    {
        private Image backgroundImage;
        public AboutUs()
        {
            InitializeComponent();
            InitializeContent();
            backgroundImage= Image.FromFile("C:\\Users\\Hp\\source\\repos\\FHP _NEW\\FHP\\Resources\\logo.png");

        }

        /// <summary>
        /// Initializes the content of the About Us form.
        /// </summary>
        private void InitializeContent()
        {
            Label label1 = new Label
            {
                Text = "Welcome to File Handling Program (FHP)!\n\n" +
                   "At Vertex, we are passionate about delivering innovative solutions that empower and enhance your experience. File Handling Program (FHP) is a result of our dedication to providing a simplify data handling app for our users\n\n" +
                   "Who We Are:\n" +
                   "The FHP Team comprises dedicated professionals committed to providing the best solutions for managing employee data.\n\n" +
                   "Key Features:\n" +
                   "- Seamless Data Organization: DataMaster Pro allows you to effortlessly organize and manage details ranging from employee names to qualifications and joining dates.\n" +
                   "- Swift Search and Filtering: Need specific information? Just type it in, and DataMaster Pro will locate it for you.\n" +
                   "- Effortless Updates and Deletions: Keep your records accurate by updating or deleting information with ease.\n\n" +
                   "Getting Started:\n" +
                   "1. Add a New Data Entry: Click on 'New' to add a new entry to your records.\n" +
                   "2. Update or Remove: Select a record and use 'Update' or 'Delete' to make necessary changes.\n" +
                   "3. Explore Details: Double-click on a record or use 'View' to see all the details about a specific entry.\n" +
                   "4. Quick Search: Utilize the search bar to find what you need swiftly.\n\n" +
                   "Contact Us:\n" +
                   "We value your feedback and are here to assist you. If you have any questions, suggestions, or concerns, please feel free to reach out to us at Email: support@datamasterpro.com\n" +
                   "\n" +
                   "Thank you for choosing FHP. We look forward to being a part of your journey!\n\n",
                Dock = DockStyle.Fill,
                Font = new System.Drawing.Font("skita text", 10),
                ForeColor = Color.Black,
                BackColor = Color.Transparent
            };

            Controls.Add(label1);
        }

        /// <summary>
        /// Handles the Paint event of the AboutUs form.
        /// </summary>
        private void AboutUs_Paint(object sender, PaintEventArgs e)
        {

            int x = (this.ClientSize.Width - backgroundImage.Width) / 2;
            int y = (this.ClientSize.Height - backgroundImage.Height) / 2;

            ImageAttributes imageAttributes = new ImageAttributes();
            imageAttributes.SetColorMatrix(new ColorMatrix
            {
                Matrix33 = 0.2f
            });

            e.Graphics.DrawImage(
                backgroundImage,
                new Rectangle(x, y, backgroundImage.Width, backgroundImage.Height),
                0, 0, backgroundImage.Width, backgroundImage.Height,
                GraphicsUnit.Pixel,
                imageAttributes
            );
        }
    }
}
