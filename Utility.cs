//
// Utility.cs
//
// Author:
//       madhur <>
//
// Copyright (c) 2016 madhur
using System;
using System.Text.RegularExpressions;

namespace SomeNameSpace
{
	public class Utility
	{

		public static int MIN_PHONE_DIGITS = 9;
		public static int MAX_PHONE_DIGITS = 13;


		public Utility ()
		{
		}

		public static bool IsPhoneNumberValid(String phone)
		{
			bool isPhoneValid = false;
			if (phone != null && (phone.Length >= MIN_PHONE_DIGITS && phone.Length <= MAX_PHONE_DIGITS)) {
				isPhoneValid = Regex.Match (phone, @"^\+?(\d[\d-. ]+)?(\([\d-. ]+\))?[\d-. ]+\d$").Success;
			}
			return isPhoneValid;
		}
	}
}

