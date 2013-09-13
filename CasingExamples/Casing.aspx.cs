using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

//Necessary for TextInfo (used in ToTitleCase)
using System.Globalization;
//Necessary for Irish and Custom-casing
using System.Text.RegularExpressions;


namespace CasingExamples
{
    public partial class Casing : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //Ignore these examples for PostBack purposes
            if (!IsPostBack)
            {
                //Example 1: ToLowerCase (all lower case)
                var lowercase = "This is an example".ToLowerInvariant(); //yields "this is an example"

                //Example 2: ToUpperCase (all upper case)
                var uppercase = "This is an example".ToUpperInvariant(); //yields "THIS IS AN EXAMPLE"

                //Example 3: Native ToTitleCase (be careful as this will need to be all lowercase to work properly)
                var nottitled = CultureInfo.InvariantCulture.TextInfo.ToTitleCase("THIS IS AN EXAMPLE"); //yields "THIS IS AN EXAMPLE"

                //Example 3b: ToTitleCase (this will convert the string to lower prior to TitleCasing it)
                var titlecase = "This is an example".ToTitleCase(); //yields "This Is An Example"

                //Example 4: ToCamelCase (basically ToTitleCase except the first character is lowercased)
                var camelcase = "This is an example".ToCamelCase(); //yields "this Is An Example"

                //Example 5: ToIrishCase (again uses TitleCase but expicitly capitalizes characters after apostrophes)
                var irishcase = "o'reilly".ToIrishCase(); //yields "O'Reilly"

                //Example 5a: ToExtendedIrishCase (like Irishcase, but it also captializes characters after hyphens as well)
                var extendedIrishcase = "kathleen hely-hutchinson".ToIrishCase(); //yields "Kathleen Hely-Hutchinson"

                //Example 6: Custom Casing (only uppercases specfic characters)
                var customcase = "This is an example".ToCustomCasing(new string[] { "e", "i" }); //yields "thIs Is an ExamplE"
            }
        }

        protected void CasingButton_Click(object sender, EventArgs e)
        {
            //Grabs the value within your Textbox and sets the values for your Examples
            var example = ExampleTextBox.Text;

            //Sets each of the appropriate textboxes
            Lower.Text = example.ToLowerInvariant();
            Upper.Text = example.ToUpperInvariant();
            NativeTitle.Text = CultureInfo.InvariantCulture.TextInfo.ToTitleCase(example);
            Title.Text = example.ToTitleCase();
            Camel.Text = example.ToCamelCase();
            Irish.Text = example.ToIrishCase();
            ExtendedIrish.Text = example.ToExtendedIrishCase();
            Custom.Text = example.ToCustomCasing(new string[] { "a","e", "i", "o", "u", "y" });
        }
    }

    //Extension methods to easily add-in the necessary functionality for each of the casing scenarios
    public static class StringExtensions
    {
        public static string ToTitleCase(this string s)
        {
            return CultureInfo.InvariantCulture.TextInfo.ToTitleCase(s.ToLowerInvariant());
        }

        public static string ToCamelCase(this string s)
        {
            //Build the titlecase string
            var titlecase = CultureInfo.InvariantCulture.TextInfo.ToTitleCase(s.ToLowerInvariant());
            //Ensures that there is atleast two characters (so that the Substring method doesn't freak out)
            return (titlecase.Length > 1) ? Char.ToLowerInvariant(titlecase[0]) + titlecase.Substring(1) : titlecase;
        }

        public static string ToIrishCase(this string s)
        {
            //This will build a Titlecased string, but will uppercase any letter's that appear after apostrophes (as in names)
            var titlecase = CultureInfo.InvariantCulture.TextInfo.ToTitleCase(s.ToLowerInvariant());
            //Replaces any character after an apostrophe with it's uppercase variant
            return Regex.Replace(titlecase,"'(?:.)", m => m.Value.ToUpperInvariant());
        }

        public static string ToExtendedIrishCase(this string s)
        {
            //This will build a Titlecased string, but will uppercase any letter's that appear after apostrophes (as in names)
            var titlecase = CultureInfo.InvariantCulture.TextInfo.ToTitleCase(s.ToLowerInvariant());
            //Replaces any character after an apostrophe or hyphen with it's uppercase variant
            return Regex.Replace(titlecase, @"['\-](?:.)", m => m.Value.ToUpperInvariant());
        }

        public static string ToCustomCasing(this string s, string[] characters)
        {
            //If there are no characters to specifically capialize - return the initial string
            if (characters == null || !characters.Any())
            {
                return s;
            }

            //Replacement expression
            var replacements = String.Format("[{0}]", String.Join("", characters).ToLowerInvariant());

            //Replaces any characters that were passed in
            return Regex.Replace(s.ToLowerInvariant(), replacements, m => m.Value.ToUpperInvariant());
        }
    }
}