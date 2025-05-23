using OfficeAnywhere.Mobile.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OfficeAnywhere.Mobile.Extensions;

public static class InputTypeExtensions
{
    public static Keyboard ToKeyboard(this InputType inputType)
    {
        return inputType switch
        {
            InputType.Text => Keyboard.Text,
            InputType.Email => Keyboard.Email,
            InputType.Numeric => Keyboard.Numeric,
            InputType.Telephone => Keyboard.Telephone,
            InputType.Url => Keyboard.Url,
            InputType.Chat => Keyboard.Chat,
            _ => Keyboard.Default
        };
    }
}
