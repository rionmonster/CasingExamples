'Required for using TextInfo (in TitleCase etc.)
Imports System.Globalization
'Required for Regular Expressions
Imports System.Text.RegularExpressions
'Required for Extension methods
Imports System.Runtime.CompilerServices

Public Class Casing
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'Ignore these examples for PostBack purposes
        If (Not IsPostBack) Then
            'Example 1: ToLowerCase (all lower case)
            Dim lowercase = "This is an example".ToLowerInvariant() 'yields "this is an example"

            'Example 2: ToUpperCase (all upper case)
            Dim uppercase = "This is an example".ToUpperInvariant() 'yields "THIS IS AN EXAMPLE"

            'Example 3: Native ToTitleCase (be careful as this will need to be all lowercase to work properly)
            Dim nottitled = CultureInfo.InvariantCulture.TextInfo.ToTitleCase("THIS IS AN EXAMPLE") 'yields "THIS IS AN EXAMPLE"

            'Example 3b: ToTitleCase (this will convert the string to lower prior to TitleCasing it)
            Dim titlecase = "This is an example".ToTitleCase() 'yields "This Is An Example"

            'Example 4: ToCamelCase (basically ToTitleCase except the first character is lowercased)
            Dim camelcase = "This is an example".ToCamelCase() 'yields "this Is An Example"

            'Example 5: ToIrishCase (again uses TitleCase but expicitly capitalizes characters after apostrophes)
            Dim irishcase = "o'reilly".ToIrishCase() 'yields "O'Reilly"

            'Example 5a: ToExtendedIrishCase (like Irishcase, but it also captializes characters after hyphens as well)
            Dim extendedIrishcase = "kathleen hely-hutchinson".ToIrishCase() 'yields "Kathleen Hely-Hutchinson"

            'Example 6: Custom Casing (only uppercases specfic characters)
            Dim customcase = "This is an example".ToCustomCasing(New String() {"e", "i"}) 'yields "thIs Is an ExamplE"
        End If
    End Sub

    Protected Sub CasingButton_Click(sender As Object, e As EventArgs) Handles CasingButton.Click
        'Grabs the value within your Textbox and sets the values for your Examples
        Dim example = ExampleTextBox.Text

        'Sets each of the appropriate textboxes
        Lower.Text = example.ToLowerInvariant()
        Upper.Text = example.ToUpperInvariant()
        NativeTitle.Text = CultureInfo.InvariantCulture.TextInfo.ToTitleCase(example)
        Title.Text = example.ToTitleCase()
        Camel.Text = example.ToCamelCase()
        Irish.Text = example.ToIrishCase()
        ExtendedIrish.Text = example.ToExtendedIrishCase()
        Custom.Text = example.ToCustomCasing(New String() {"a", "e", "i", "o", "u", "y"})
    End Sub
End Class

'Extension methods to easily add-in the necessary functionality for each of the casing scenarios
Module StringExtensions

    <Extension()>
    Public Function ToTitleCase(ByVal s As String) As String
        Return CultureInfo.InvariantCulture.TextInfo.ToTitleCase(s.ToLowerInvariant())
    End Function

    <Extension()>
    Public Function ToCamelCase(ByVal s As String) As String
        'Build the titlecase string
        Dim titlecase = CultureInfo.InvariantCulture.TextInfo.ToTitleCase(s.ToLowerInvariant())
        'Ensures that there is atleast two characters (so that the Substring method doesn't freak out)
        Return IIf(titlecase.Length > 1, Char.ToLowerInvariant(titlecase(0)) + titlecase.Substring(1), titlecase)
    End Function

    <Extension()>
    Public Function ToIrishCase(ByVal s As String) As String
        'This will build a Titlecased string, but will uppercase any letter's that appear after apostrophes (as in names)
        Dim titlecase = CultureInfo.InvariantCulture.TextInfo.ToTitleCase(s.ToLowerInvariant())
        'Replaces any character after an apostrophe with it's uppercase variant
        Return Regex.Replace(titlecase, "'(?:.)", Function(m) m.Value.ToUpperInvariant())
    End Function

    <Extension()>
    Public Function ToExtendedIrishCase(ByVal s As String) As String
        'This will build a Titlecased string, but will uppercase any letter's that appear after apostrophes (as in names)
        Dim titlecase = CultureInfo.InvariantCulture.TextInfo.ToTitleCase(s.ToLowerInvariant())
        'Replaces any character after an apostrophe or hyphen with it's uppercase variant
        Return Regex.Replace(titlecase, "['\-](?:.)", Function(m) m.Value.ToUpperInvariant())
    End Function

    <Extension()>
    Public Function ToCustomCasing(ByVal s As String, ByVal characters As String()) As String
        'If there are no characters to specifically capialize - return the initial string
        If characters Is Nothing OrElse Not characters.Any() Then
            Return s
        End If

        'Replacement expression
        Dim replacements = String.Format("[{0}]", String.Join("", characters).ToLowerInvariant())

        'Replaces any characters that were passed in
        Return Regex.Replace(s.ToLowerInvariant(), replacements, Function(m) m.Value.ToUpperInvariant())
    End Function

End Module