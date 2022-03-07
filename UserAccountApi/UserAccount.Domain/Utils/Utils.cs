using System;
using System.Text.RegularExpressions;

namespace UserAccount.Domain.Utils;

public static class Utils
{
	public static bool IsEmail(string email)
	{
		Regex regex = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
		Match match = regex.Match(email);

		return match.Success;
	}

	public static bool IsPassword(string password)
    {
		Regex regex = new Regex(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[#$^+=!*()@%&]).{8,32}$");
		Match match = regex.Match(password);
		  
		return match.Success;
	}
}

