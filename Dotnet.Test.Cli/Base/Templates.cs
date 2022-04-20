using Sharprompt;
using Sharprompt.Fluent;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dotnet.Test.Cli.TEMPLATES_REFERENCE
{
    public class Templates
    {
        public void SimpleInput()
        {
            // alphanumeric
            var name = Prompt.Input<string>("What's your name?");
            Console.WriteLine($"Hello, {name}!");

            // numeric
            var number = Prompt.Input<int>("Enter any number");
            Console.WriteLine($"Input = {number}");
        }

        public void PasswordInput()
        {
            var secret = Prompt.Password("Type new password", validators: new[] { Validators.Required(), Validators.MinLength(8) });
            Console.WriteLine("Password OK");
        }

        public void Confirmation()
        {
            var answer = Prompt.Confirm("Are you ready?", defaultValue: true);
            Console.WriteLine($"Your answer is {answer}");
        }

        public void Select()
        {
            var city = Prompt.Select("Select your city", new[] { "Seattle", "London", "Tokyo" });
            Console.WriteLine($"Hello, {city}!");
        }

        public void MultiSelect()
        {
            var cities = Prompt.MultiSelect("Which cities would you like to visit?", new[] { "Seattle", "London", "Tokyo", "New York", "Singapore", "Shanghai" }, pageSize: 3);
            Console.WriteLine($"You picked {string.Join(", ", cities)}");
        }

        public void List()
        {
            var value = Prompt.List<string>("Please add item(s)");
            Console.WriteLine($"You picked {string.Join(", ", value)}");
        }

        public void ModelBinding()
        {
            var result = Prompt.Bind<MyFormModel>();

            Prompt.Symbols.Prompt = new Symbol("🤔", "?");
            Prompt.Symbols.Done = new Symbol("😎", "V");
            Prompt.Symbols.Error = new Symbol("😱", ">>");

            var name = Prompt.Input<string>("What's your name?");
            Console.WriteLine($"Hello, {name}!");
        }

        public void Colors()
        {

            Prompt.ColorSchema.Answer = ConsoleColor.DarkRed;
            Prompt.ColorSchema.Select = ConsoleColor.DarkCyan;

            var name = Prompt.Input<string>("What's your name?");
            Console.WriteLine($"Hello, {name}!");
        }

        public void ThrowOnCancel()
        {
            //Throw an exception when canceling with Ctrl - C
            Prompt.ThrowExceptionOnCancel = true;

            try
            {
                var name = Prompt.Input<string>("What's your name?");
                Console.WriteLine($"Hello, {name}!");
            }
            catch
            {
                Console.WriteLine("Prompt canceled");
            }
        }

        public void Enums()
        {
            var value = Prompt.Select<MyEnum>("Select enum value");
            Console.WriteLine($"You selected {value}");
        }

        public void Encoding()
        {
            //Prefer UTF-8 as the output encoding
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            var name = Prompt.Input<string>("What's your name?");
            Console.WriteLine($"Hello, {name}!");
        }

        public void Fluent()
        {
            // Use fluent interface
            var city = Prompt.Select<string>(o => o.WithMessage("Select your city")
                                                   .WithItems(new[] { "Seattle", "London", "Tokyo" })
                                                   .WithDefaultValue("Seattle"));
        }
    }

    public enum MyEnum
    {
        [Display(Name = "First value")]
        First,
        [Display(Name = "Second value")]
        Second,
        [Display(Name = "Third value")]
        Third
    }

    // Input model definition
    public class MyFormModel
    {
        [Display(Name = "What's your name?")]
        [Required]
        public string Name { get; set; } = string.Empty;

        [Display(Name = "Type new password")]
        [DataType(DataType.Password)]
        [Required]
        [MinLength(8)]
        public string Password { get; set; } = string.Empty;

        [Display(Name = "Select your city")]
        [Required]
        [InlineItems("Seattle", "London", "Tokyo")]
        public string City { get; set; } = string.Empty;

        [Display(Name = "Are you ready?")]
        public bool? Ready { get; set; }
    }
}

