//-----------------------------------------------------------------------------
/// <summary>
/// This is an Open Source File Created by: Abdullah Konash. Twitter: @konash
/// This File allow the users to use arabic text in XNA and Unity platform.
/// It flips the characters and replace them with the appropriate ones to connect the letters in the correct way.
/// 
/// The project is available on GitHub here: https://github.com/Konash/arabic-support-unity
/// Unity Asset Store link: https://www.assetstore.unity3d.com/en/#!/content/2674
/// Please help in improving the plugin. 
/// 
/// I would love to see the work you use this plugin for. Send me a copy at: abdullah.konash[at]gmail[dot]com
/// </summary>
/// 
/// <license>
/// MIT License
/// 
/// Copyright(c) 2018
/// Abdullah Konash
/// 
/// Permission is hereby granted, free of charge, to any person obtaining a copy
/// of this software and associated documentation files (the "Software"), to deal
/// /// in the Software without restriction, including without limitation the rights
/// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
/// copies of the Software, and to permit persons to whom the Software is
/// furnished to do so, subject to the following conditions:
/// 
/// The above copyright notice and this permission notice shall be included in all
/// copies or substantial portions of the Software.
/// 
/// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
/// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
/// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
/// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
/// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
/// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
/// SOFTWARE.
/// </license>

//-----------------------------------------------------------------------------


#region Using Statements
using System;
using System.Collections.Generic;
#endregion

	public class ArabicLangSupport
{
		/// <summary>
		/// Fix the specified string.
		/// </summary>
		/// <param name='str'>
		/// String to be fixed.
		/// </param>
		public static string Fix(string str)
		{
			return Fix(str, false, true);
		}

		public static string Fix(string str, bool rtl)
		{
			if (rtl)

			{
				return Fix(str);
			}
			else
			{
				string[] words = str.Split(' ');
				string result = "";
				string arabicToIgnore = "";
				foreach (string word in words)
				{
					if (char.IsLower(word.ToLower()[word.Length / 2]))
					{
						result += Fix(arabicToIgnore) + word + " ";
						arabicToIgnore = "";
					}
					else
					{
						arabicToIgnore += word + " ";

					}
				}
				if (arabicToIgnore != "")
					result += Fix(arabicToIgnore);

				return result;
			}
		}

		/// <summary>
		/// Fix the specified string with customization options.
		/// </summary>
		/// <param name='str'>
		/// String to be fixed.
		/// </param>
		/// <param name='showTashkeel'>
		/// Show tashkeel.
		/// </param>
		/// <param name='useHinduNumbers'>
		/// Use hindu numbers.
		/// </param>
		public static string Fix(string str, bool showTashkeel, bool useHinduNumbers)
		{
			ArabicLangFixerTool.showTashkeel = showTashkeel;
			ArabicLangFixerTool.useHinduNumbers = useHinduNumbers;

			if (str.Contains("\n"))
				str = str.Replace("\n", Environment.NewLine);

			if (str.Contains(Environment.NewLine))
			{
				string[] stringSeparators = new string[] { Environment.NewLine };
				string[] strSplit = str.Split(stringSeparators, StringSplitOptions.None);

				if (strSplit.Length == 0)
					return ArabicLangFixerTool.FixLine(str);
				else if (strSplit.Length == 1)
					return ArabicLangFixerTool.FixLine(str);
				else
				{
					string outputString = ArabicLangFixerTool.FixLine(strSplit[0]);
					int iteration = 1;
					if (strSplit.Length > 1)
					{
						while (iteration < strSplit.Length)
						{
							outputString += Environment.NewLine + ArabicLangFixerTool.FixLine(strSplit[iteration]);
							iteration++;
						}
					}
					return outputString;
				}
			}
			else
			{
				return ArabicLangFixerTool.FixLine(str);
			}

		}

		public static string Fix(string str, bool showTashkeel, bool combineTashkeel, bool useHinduNumbers)
		{
			ArabicLangFixerTool.combineTashkeel = combineTashkeel;
			return Fix(str, showTashkeel, useHinduNumbers);
		}


	}


/// <summary>
/// ArabicLang Contextual forms General - Unicode
/// </summary>
internal enum IsolatedArabicLangLetters
{
	Hamza = 0xFE80,
	Alef = 0xFE8D,
	AlefHamza = 0xFE83,
	WawHamza = 0xFE85,
	AlefMaksoor = 0xFE87,
	AlefMaksora = 0xFBFC,
	HamzaNabera = 0xFE89,
	Ba = 0xFE8F,
	Ta = 0xFE95,
	Tha2 = 0xFE99,
	Jeem = 0xFE9D,
	H7aa = 0xFEA1,
	Khaa2 = 0xFEA5,
	Dal = 0xFEA9,
	Thal = 0xFEAB,
	Ra2 = 0xFEAD,
	Zeen = 0xFEAF,
	Seen = 0xFEB1,
	Sheen = 0xFEB5,
	S9a = 0xFEB9,
	Dha = 0xFEBD,
	T6a = 0xFEC1,
	T6ha = 0xFEC5,
	Ain = 0xFEC9,
	Gain = 0xFECD,
	Fa = 0xFED1,
	Gaf = 0xFED5,
	Kaf = 0xFED9,
	Lam = 0xFEDD,
	Meem = 0xFEE1,
	Noon = 0xFEE5,
	Ha = 0xFEE9,
	Waw = 0xFEED,
	Ya = 0xFEF1,
	AlefMad = 0xFE81,
	TaMarboota = 0xFE93,
	PersianPe = 0xFB56,     // Persian Letters;
	PersianChe = 0xFB7A,
	PersianZe = 0xFB8A,
	PersianGaf = 0xFB92,
	PersianGaf2 = 0xFB8E,
	PersianYeh = 0xFBFC,

}

/// <summary>
/// ArabicLang Contextual forms - Isolated
/// </summary>
internal enum GeneralArabicLangLetters
{
	Hamza = 0x0621,
	Alef = 0x0627,
	AlefHamza = 0x0623,
	WawHamza = 0x0624,
	AlefMaksoor = 0x0625,
	AlefMagsora = 0x0649,
	HamzaNabera = 0x0626,
	Ba = 0x0628,
	Ta = 0x062A,
	Tha2 = 0x062B,
	Jeem = 0x062C,
	H7aa = 0x062D,
	Khaa2 = 0x062E,
	Dal = 0x062F,
	Thal = 0x0630,
	Ra2 = 0x0631,
	Zeen = 0x0632,
	Seen = 0x0633,
	Sheen = 0x0634,
	S9a = 0x0635,
	Dha = 0x0636,
	T6a = 0x0637,
	T6ha = 0x0638,
	Ain = 0x0639,
	Gain = 0x063A,
	Fa = 0x0641,
	Gaf = 0x0642,
	Kaf = 0x0643,
	Lam = 0x0644,
	Meem = 0x0645,
	Noon = 0x0646,
	Ha = 0x0647,
	Waw = 0x0648,
	Ya = 0x064A,
	AlefMad = 0x0622,
	TaMarboota = 0x0629,
	PersianPe = 0x067E,     // Persian Letters;
	PersianChe = 0x0686,
	PersianZe = 0x0698,
	PersianGaf = 0x06AF,
	PersianGaf2 = 0x06A9,
	PersianYeh = 0x06CC,

}

/// <summary>
/// Data Structure for conversion
/// </summary>
internal class ArabicLangMapping
{
	public int from;
	public int to;
	public ArabicLangMapping(int from, int to)
	{
		this.from = from;
		this.to = to;
	}
}

/// <summary>
/// Sets up and creates the conversion table 
/// </summary>
internal class ArabicLangTable
{

	private static List<ArabicLangMapping> mapList;
	public static ArabicLangTable ArabicLangMapper;

	/// <summary>
	/// Setting up the conversion table
	/// </summary>
	private ArabicLangTable()
	{
		mapList = new List<ArabicLangMapping>();



		mapList.Add(new ArabicLangMapping((int)GeneralArabicLangLetters.Hamza, (int)IsolatedArabicLangLetters.Hamza));
		mapList.Add(new ArabicLangMapping((int)GeneralArabicLangLetters.Alef, (int)IsolatedArabicLangLetters.Alef));
		mapList.Add(new ArabicLangMapping((int)GeneralArabicLangLetters.AlefHamza, (int)IsolatedArabicLangLetters.AlefHamza));
		mapList.Add(new ArabicLangMapping((int)GeneralArabicLangLetters.WawHamza, (int)IsolatedArabicLangLetters.WawHamza));
		mapList.Add(new ArabicLangMapping((int)GeneralArabicLangLetters.AlefMaksoor, (int)IsolatedArabicLangLetters.AlefMaksoor));
		mapList.Add(new ArabicLangMapping((int)GeneralArabicLangLetters.AlefMagsora, (int)IsolatedArabicLangLetters.AlefMaksora));
		mapList.Add(new ArabicLangMapping((int)GeneralArabicLangLetters.HamzaNabera, (int)IsolatedArabicLangLetters.HamzaNabera));
		mapList.Add(new ArabicLangMapping((int)GeneralArabicLangLetters.Ba, (int)IsolatedArabicLangLetters.Ba));
		mapList.Add(new ArabicLangMapping((int)GeneralArabicLangLetters.Ta, (int)IsolatedArabicLangLetters.Ta));
		mapList.Add(new ArabicLangMapping((int)GeneralArabicLangLetters.Tha2, (int)IsolatedArabicLangLetters.Tha2));
		mapList.Add(new ArabicLangMapping((int)GeneralArabicLangLetters.Jeem, (int)IsolatedArabicLangLetters.Jeem));
		mapList.Add(new ArabicLangMapping((int)GeneralArabicLangLetters.H7aa, (int)IsolatedArabicLangLetters.H7aa));
		mapList.Add(new ArabicLangMapping((int)GeneralArabicLangLetters.Khaa2, (int)IsolatedArabicLangLetters.Khaa2));
		mapList.Add(new ArabicLangMapping((int)GeneralArabicLangLetters.Dal, (int)IsolatedArabicLangLetters.Dal));
		mapList.Add(new ArabicLangMapping((int)GeneralArabicLangLetters.Thal, (int)IsolatedArabicLangLetters.Thal));
		mapList.Add(new ArabicLangMapping((int)GeneralArabicLangLetters.Ra2, (int)IsolatedArabicLangLetters.Ra2));
		mapList.Add(new ArabicLangMapping((int)GeneralArabicLangLetters.Zeen, (int)IsolatedArabicLangLetters.Zeen));
		mapList.Add(new ArabicLangMapping((int)GeneralArabicLangLetters.Seen, (int)IsolatedArabicLangLetters.Seen));
		mapList.Add(new ArabicLangMapping((int)GeneralArabicLangLetters.Sheen, (int)IsolatedArabicLangLetters.Sheen));
		mapList.Add(new ArabicLangMapping((int)GeneralArabicLangLetters.S9a, (int)IsolatedArabicLangLetters.S9a));
		mapList.Add(new ArabicLangMapping((int)GeneralArabicLangLetters.Dha, (int)IsolatedArabicLangLetters.Dha));
		mapList.Add(new ArabicLangMapping((int)GeneralArabicLangLetters.T6a, (int)IsolatedArabicLangLetters.T6a));
		mapList.Add(new ArabicLangMapping((int)GeneralArabicLangLetters.T6ha, (int)IsolatedArabicLangLetters.T6ha));
		mapList.Add(new ArabicLangMapping((int)GeneralArabicLangLetters.Ain, (int)IsolatedArabicLangLetters.Ain));
		mapList.Add(new ArabicLangMapping((int)GeneralArabicLangLetters.Gain, (int)IsolatedArabicLangLetters.Gain));
		mapList.Add(new ArabicLangMapping((int)GeneralArabicLangLetters.Fa, (int)IsolatedArabicLangLetters.Fa));
		mapList.Add(new ArabicLangMapping((int)GeneralArabicLangLetters.Gaf, (int)IsolatedArabicLangLetters.Gaf));
		mapList.Add(new ArabicLangMapping((int)GeneralArabicLangLetters.Kaf, (int)IsolatedArabicLangLetters.Kaf));
		mapList.Add(new ArabicLangMapping((int)GeneralArabicLangLetters.Lam, (int)IsolatedArabicLangLetters.Lam));
		mapList.Add(new ArabicLangMapping((int)GeneralArabicLangLetters.Meem, (int)IsolatedArabicLangLetters.Meem));
		mapList.Add(new ArabicLangMapping((int)GeneralArabicLangLetters.Noon, (int)IsolatedArabicLangLetters.Noon));
		mapList.Add(new ArabicLangMapping((int)GeneralArabicLangLetters.Ha, (int)IsolatedArabicLangLetters.Ha));
		mapList.Add(new ArabicLangMapping((int)GeneralArabicLangLetters.Waw, (int)IsolatedArabicLangLetters.Waw));
		mapList.Add(new ArabicLangMapping((int)GeneralArabicLangLetters.Ya, (int)IsolatedArabicLangLetters.Ya));
		mapList.Add(new ArabicLangMapping((int)GeneralArabicLangLetters.AlefMad, (int)IsolatedArabicLangLetters.AlefMad));
		mapList.Add(new ArabicLangMapping((int)GeneralArabicLangLetters.TaMarboota, (int)IsolatedArabicLangLetters.TaMarboota));
		mapList.Add(new ArabicLangMapping((int)GeneralArabicLangLetters.PersianPe, (int)IsolatedArabicLangLetters.PersianPe));      // Persian Letters;
		mapList.Add(new ArabicLangMapping((int)GeneralArabicLangLetters.PersianChe, (int)IsolatedArabicLangLetters.PersianChe));
		mapList.Add(new ArabicLangMapping((int)GeneralArabicLangLetters.PersianZe, (int)IsolatedArabicLangLetters.PersianZe));
		mapList.Add(new ArabicLangMapping((int)GeneralArabicLangLetters.PersianGaf, (int)IsolatedArabicLangLetters.PersianGaf));
		mapList.Add(new ArabicLangMapping((int)GeneralArabicLangLetters.PersianGaf2, (int)IsolatedArabicLangLetters.PersianGaf2));
		mapList.Add(new ArabicLangMapping((int)GeneralArabicLangLetters.PersianYeh, (int)IsolatedArabicLangLetters.PersianYeh));




		//for (int i = 0; i < generalArabic.Length; i++)
		//    mapList.Add(new ArabicMapping((int)generalArabic.GetValue(i), (int)isolatedArabic.GetValue(i)));    // I


	}

	/// <summary>
	/// Singleton design pattern, Get the mapper. If it was not created before, create it.
	/// </summary>
	internal static ArabicLangTable arabicMapper
	{
		get
		{
			return arabicMapper;
		}
	}

	internal int Convert(int toBeConverted)
	{

		foreach (ArabicLangMapping arabicMap in mapList)
			if (arabicMap.from == toBeConverted)
			{
				return arabicMap.to;
			}
		return toBeConverted;
	}


}


internal class TashkeelLangLocation
{
	public char tashkeel;
	public int position;
	public TashkeelLangLocation(char tashkeel, int position)
	{
		this.tashkeel = tashkeel;
		this.position = position;
	}
}


internal class ArabicLangFixerTool
{
	internal static bool showTashkeel = true;
	internal static bool combineTashkeel = true;
	internal static bool useHinduNumbers = false;


	internal static string RemoveTashkeel(string str, out List<TashkeelLangLocation> TashkeelLangLocation)
	{
		TashkeelLangLocation = new List<TashkeelLangLocation>();
		char[] letters = str.ToCharArray();

		int index = 0;
		for (int i = 0; i < letters.Length; i++)
		{
			if (letters[i] == (char)0x064B)
			{ // Tanween Fatha
				TashkeelLangLocation.Add(new TashkeelLangLocation((char)0x064B, i));
				index++;
			}
			else if (letters[i] == (char)0x064C)
			{ // Tanween Damma
				TashkeelLangLocation.Add(new TashkeelLangLocation((char)0x064C, i));
				index++;
			}
			else if (letters[i] == (char)0x064D)
			{ // Tanween Kasra
				TashkeelLangLocation.Add(new TashkeelLangLocation((char)0x064D, i));
				index++;
			}
			else if (letters[i] == (char)0x064E)
			{ // Fatha
				if (index > 0 && combineTashkeel)
				{
					if (TashkeelLangLocation[index - 1].tashkeel == (char)0x0651) // Shadda
					{
						TashkeelLangLocation[index - 1].tashkeel = (char)0xFC60; // Shadda With Fatha
						continue;
					}
				}

				TashkeelLangLocation.Add(new TashkeelLangLocation((char)0x064E, i));
				index++;
			}
			else if (letters[i] == (char)0x064F)
			{ // DAMMA
				if (index > 0 && combineTashkeel)
				{
					if (TashkeelLangLocation[index - 1].tashkeel == (char)0x0651)
					{ // SHADDA
						TashkeelLangLocation[index - 1].tashkeel = (char)0xFC61; // Shadda With DAMMA
						continue;
					}
				}
				TashkeelLangLocation.Add(new TashkeelLangLocation((char)0x064F, i));
				index++;
			}
			else if (letters[i] == (char)0x0650)
			{ // KASRA
				if (index > 0 && combineTashkeel)
				{
					if (TashkeelLangLocation[index - 1].tashkeel == (char)0x0651)
					{ // SHADDA
						TashkeelLangLocation[index - 1].tashkeel = (char)0xFC62; // Shadda With KASRA
						continue;
					}
				}
				TashkeelLangLocation.Add(new TashkeelLangLocation((char)0x0650, i));
				index++;
			}
			else if (letters[i] == (char)0x0651)
			{ // SHADDA
				if (index > 0 && combineTashkeel)
				{
					if (TashkeelLangLocation[index - 1].tashkeel == (char)0x064E) // FATHA
					{
						TashkeelLangLocation[index - 1].tashkeel = (char)0xFC60; // Shadda With Fatha
						continue;
					}

					if (TashkeelLangLocation[index - 1].tashkeel == (char)0x064F) // DAMMA
					{
						TashkeelLangLocation[index - 1].tashkeel = (char)0xFC61; // Shadda With DAMMA
						continue;
					}

					if (TashkeelLangLocation[index - 1].tashkeel == (char)0x0650) // KASRA
					{
						TashkeelLangLocation[index - 1].tashkeel = (char)0xFC62; // Shadda With KASRA
						continue;
					}
				}

				TashkeelLangLocation.Add(new TashkeelLangLocation((char)0x0651, i));
				index++;
			}
			else if (letters[i] == (char)0x0652)
			{ // SUKUN
				TashkeelLangLocation.Add(new TashkeelLangLocation((char)0x0652, i));
				index++;
			}
			else if (letters[i] == (char)0x0653)
			{ // MADDAH ABOVE
				TashkeelLangLocation.Add(new TashkeelLangLocation((char)0x0653, i));
				index++;
			}
		}

		string[] split = str.Split(new char[]{(char)0x064B,(char)0x064C,(char)0x064D,
			(char)0x064E,(char)0x064F,(char)0x0650,

			(char)0x0651,(char)0x0652,(char)0x0653,(char)0xFC60,(char)0xFC61,(char)0xFC62});
		str = "";

		foreach (string s in split)
		{
			str += s;
		}

		return str;
	}

	internal static char[] ReturnTashkeel(char[] letters, List<TashkeelLangLocation> TashkeelLangLocation)
	{
		char[] lettersWithTashkeel = new char[letters.Length + TashkeelLangLocation.Count];

		int letterWithTashkeelTracker = 0;
		for (int i = 0; i < letters.Length; i++)
		{
			lettersWithTashkeel[letterWithTashkeelTracker] = letters[i];
			letterWithTashkeelTracker++;
			foreach (TashkeelLangLocation hLocation in TashkeelLangLocation)
			{
				if (hLocation.position == letterWithTashkeelTracker)
				{
					lettersWithTashkeel[letterWithTashkeelTracker] = hLocation.tashkeel;
					letterWithTashkeelTracker++;
				}
			}
		}

		return lettersWithTashkeel;
	}

	/// <summary>
	/// Converts a string to a form in which the sting will be displayed correctly for arabic text.
	/// </summary>
	/// <param name="str">String to be converted. Example: "Aaa"</param>
	/// <returns>Converted string. Example: "aa aaa A" without the spaces.</returns>
	internal static string FixLine(string str)
	{
		string test = "";

		List<TashkeelLangLocation> TashkeelLangLocation;

		string originString = RemoveTashkeel(str, out TashkeelLangLocation);

		char[] lettersOrigin = originString.ToCharArray();
		char[] lettersFinal = originString.ToCharArray();



		for (int i = 0; i < lettersOrigin.Length; i++)
		{
			lettersOrigin[i] = (char)ArabicLangTable.ArabicLangMapper.Convert(lettersOrigin[i]);
		}

		for (int i = 0; i < lettersOrigin.Length; i++)
		{
			bool skip = false;


			//lettersOrigin[i] = (char)ArabicTable.ArabicMapper.Convert(lettersOrigin[i]);


			// For special Lam Letter connections.
			if (lettersOrigin[i] == (char)IsolatedArabicLangLetters.Lam)
			{

				if (i < lettersOrigin.Length - 1)
				{
					//lettersOrigin[i + 1] = (char)ArabicLangLangLangTable.ArabicLangLangMapper.Convert(lettersOrigin[i + 1]);
					if ((lettersOrigin[i + 1] == (char)IsolatedArabicLangLetters.AlefMaksoor))
					{
						lettersOrigin[i] = (char)0xFEF7;
						lettersFinal[i + 1] = (char)0xFFFF;
						skip = true;
					}
					else if ((lettersOrigin[i + 1] == (char)IsolatedArabicLangLetters.Alef))
					{
						lettersOrigin[i] = (char)0xFEF9;
						lettersFinal[i + 1] = (char)0xFFFF;
						skip = true;
					}
					else if ((lettersOrigin[i + 1] == (char)IsolatedArabicLangLetters.AlefHamza))
					{
						lettersOrigin[i] = (char)0xFEF5;
						lettersFinal[i + 1] = (char)0xFFFF;
						skip = true;
					}
					else if ((lettersOrigin[i + 1] == (char)IsolatedArabicLangLetters.AlefMad))
					{
						lettersOrigin[i] = (char)0xFEF3;
						lettersFinal[i + 1] = (char)0xFFFF;
						skip = true;
					}
				}

			}


			if (!IsIgnoredCharacter(lettersOrigin[i]))
			{
				if (IsMiddleLetter(lettersOrigin, i))
					lettersFinal[i] = (char)(lettersOrigin[i] + 3);
				else if (IsFinishingLetter(lettersOrigin, i))
					lettersFinal[i] = (char)(lettersOrigin[i] + 1);
				else if (IsLeadingLetter(lettersOrigin, i))
					lettersFinal[i] = (char)(lettersOrigin[i] + 2);
			}

			//string strOut = String.Format(@"\x{0:x4}", (ushort)lettersOrigin[i]);
			//UnityEngine.Debug.Log(strOut);

			//strOut = String.Format(@"\x{0:x4}", (ushort)lettersFinal[i]);
			//UnityEngine.Debug.Log(strOut);

			test += Convert.ToString((int)lettersOrigin[i], 16) + " ";
			if (skip)
				i++;


			//chaning numbers to hindu
			if (useHinduNumbers)
			{
				if (lettersOrigin[i] == (char)0x0030)
					lettersFinal[i] = (char)0x0660;
				else if (lettersOrigin[i] == (char)0x0031)
					lettersFinal[i] = (char)0x0661;
				else if (lettersOrigin[i] == (char)0x0032)
					lettersFinal[i] = (char)0x0662;
				else if (lettersOrigin[i] == (char)0x0033)
					lettersFinal[i] = (char)0x0663;
				else if (lettersOrigin[i] == (char)0x0034)
					lettersFinal[i] = (char)0x0664;
				else if (lettersOrigin[i] == (char)0x0035)
					lettersFinal[i] = (char)0x0665;
				else if (lettersOrigin[i] == (char)0x0036)
					lettersFinal[i] = (char)0x0666;
				else if (lettersOrigin[i] == (char)0x0037)
					lettersFinal[i] = (char)0x0667;
				else if (lettersOrigin[i] == (char)0x0038)
					lettersFinal[i] = (char)0x0668;
				else if (lettersOrigin[i] == (char)0x0039)
					lettersFinal[i] = (char)0x0669;
			}

		}



		//Return the Tashkeel to their places.
		if (showTashkeel)
			lettersFinal = ReturnTashkeel(lettersFinal, TashkeelLangLocation);


		List<char> list = new List<char>();

		List<char> numberList = new List<char>();

		for (int i = lettersFinal.Length - 1; i >= 0; i--)
		{


			//				if (lettersFinal[i] == '(')
			//						numberList.Add(')');
			//				else if (lettersFinal[i] == ')')
			//					numberList.Add('(');
			//				else if (lettersFinal[i] == '<')
			//					numberList.Add('>');
			//				else if (lettersFinal[i] == '>')
			//					numberList.Add('<');
			//				else 
			if (char.IsPunctuation(lettersFinal[i]) && i > 0 && i < lettersFinal.Length - 1 &&
				(char.IsPunctuation(lettersFinal[i - 1]) || char.IsPunctuation(lettersFinal[i + 1])))
			{
				if (lettersFinal[i] == '(')
					list.Add(')');
				else if (lettersFinal[i] == ')')
					list.Add('(');
				else if (lettersFinal[i] == '<')
					list.Add('>');
				else if (lettersFinal[i] == '>')
					list.Add('<');
				else if (lettersFinal[i] == '[')
					list.Add(']');
				else if (lettersFinal[i] == ']')
					list.Add('[');
				else if (lettersFinal[i] != 0xFFFF)
					list.Add(lettersFinal[i]);
			}
			// For cases where english words and arabic are mixed. This allows for using arabic, english and numbers in one sentence.
			else if (lettersFinal[i] == ' ' && i > 0 && i < lettersFinal.Length - 1 &&
					(char.IsLower(lettersFinal[i - 1]) || char.IsUpper(lettersFinal[i - 1]) || char.IsNumber(lettersFinal[i - 1])) &&
					(char.IsLower(lettersFinal[i + 1]) || char.IsUpper(lettersFinal[i + 1]) || char.IsNumber(lettersFinal[i + 1])))

			{
				numberList.Add(lettersFinal[i]);
			}

			else if (char.IsNumber(lettersFinal[i]) || char.IsLower(lettersFinal[i]) ||
					 char.IsUpper(lettersFinal[i]) || char.IsSymbol(lettersFinal[i]) ||
					 char.IsPunctuation(lettersFinal[i]))// || lettersFinal[i] == '^') //)
			{

				if (lettersFinal[i] == '(')
					numberList.Add(')');
				else if (lettersFinal[i] == ')')
					numberList.Add('(');
				else if (lettersFinal[i] == '<')
					numberList.Add('>');
				else if (lettersFinal[i] == '>')
					numberList.Add('<');
				else if (lettersFinal[i] == '[')
					list.Add(']');
				else if (lettersFinal[i] == ']')
					list.Add('[');
				else
					numberList.Add(lettersFinal[i]);
			}
			else if ((lettersFinal[i] >= (char)0xD800 && lettersFinal[i] <= (char)0xDBFF) ||
					(lettersFinal[i] >= (char)0xDC00 && lettersFinal[i] <= (char)0xDFFF))
			{
				numberList.Add(lettersFinal[i]);
			}
			else
			{
				if (numberList.Count > 0)
				{
					for (int j = 0; j < numberList.Count; j++)
						list.Add(numberList[numberList.Count - 1 - j]);
					numberList.Clear();
				}
				if (lettersFinal[i] != 0xFFFF && lettersFinal[i] != '\0')
					list.Add(lettersFinal[i]);

			}
		}
		if (numberList.Count > 0)
		{
			for (int j = 0; j < numberList.Count; j++)
				list.Add(numberList[numberList.Count - 1 - j]);
			numberList.Clear();
		}

		// Moving letters from a list to an array.
		lettersFinal = new char[list.Count];
		for (int i = 0; i < lettersFinal.Length; i++)
			lettersFinal[i] = list[i];


		str = new string(lettersFinal);
		return str;
	}

	/// <summary>
	/// English letters, numbers and punctuation characters are ignored. This checks if the ch is an ignored character.
	/// </summary>
	/// <param name="ch">The character to be checked for skipping</param>
	/// <returns>True if the character should be ignored, false if it should not be ignored.</returns>
	internal static bool IsIgnoredCharacter(char ch)
	{
		bool isPunctuation = char.IsPunctuation(ch);
		bool isNumber = char.IsNumber(ch);
		bool isLower = char.IsLower(ch);
		bool isUpper = char.IsUpper(ch);
		bool isSymbol = char.IsSymbol(ch);
		bool isPersianCharacter = ch == (char)0xFB56 || ch == (char)0xFB7A || ch == (char)0xFB8A || ch == (char)0xFB92 || ch == (char)0xFB8E;
		bool isPresentationFormB = (ch <= (char)0xFEFF && ch >= (char)0xFE70);
		bool isAcceptableCharacter = isPresentationFormB || isPersianCharacter || ch == (char)0xFBFC;



		return isPunctuation ||
			isNumber ||
				isLower ||
				isUpper ||
				isSymbol ||
				!isAcceptableCharacter ||
				ch == 'a' || ch == '>' || ch == '<' || ch == (char)0x061B;

		//            return char.IsPunctuation(ch) || char.IsNumber(ch) || ch == 'a' || ch == '>' || ch == '<' ||
		//                    char.IsLower(ch) || char.IsUpper(ch) || ch == (char)0x061B || char.IsSymbol(ch)
		//					|| !(ch <= (char)0xFEFF && ch >= (char)0xFE70) // Presentation Form B
		//					|| ch == (char)0xFB56 || ch == (char)0xFB7A || ch == (char)0xFB8A || ch == (char)0xFB92; // Persian Characters

		//					PersianPe = 0xFB56,
		//		PersianChe = 0xFB7A,
		//		PersianZe = 0xFB8A,
		//		PersianGaf = 0xFB92
		//lettersOrigin[i] <= (char)0xFEFF && lettersOrigin[i] >= (char)0xFE70
	}

	/// <summary>
	/// Checks if the letter at index value is a leading character in Arabic or not.
	/// </summary>
	/// <param name="letters">The whole word that contains the character to be checked</param>
	/// <param name="index">The index of the character to be checked</param>
	/// <returns>True if the character at index is a leading character, else, returns false</returns>
	internal static bool IsLeadingLetter(char[] letters, int index)
	{

		bool lettersThatCannotBeBeforeALeadingLetter = index == 0
			|| letters[index - 1] == ' '
				|| letters[index - 1] == '*' // ??? Remove?
				|| letters[index - 1] == 'A' // ??? Remove?
				|| char.IsPunctuation(letters[index - 1])
				|| letters[index - 1] == '>'
				|| letters[index - 1] == '<'
				|| letters[index - 1] == (int)IsolatedArabicLangLetters.Alef
				|| letters[index - 1] == (int)IsolatedArabicLangLetters.Dal
				|| letters[index - 1] == (int)IsolatedArabicLangLetters.Thal
				|| letters[index - 1] == (int)IsolatedArabicLangLetters.Ra2
				|| letters[index - 1] == (int)IsolatedArabicLangLetters.Zeen
				|| letters[index - 1] == (int)IsolatedArabicLangLetters.PersianZe
				//|| letters[index - 1] == (int)IsolatedArabicLangLetters.AlefMaksora 
				|| letters[index - 1] == (int)IsolatedArabicLangLetters.Waw
				|| letters[index - 1] == (int)IsolatedArabicLangLetters.AlefMad
				|| letters[index - 1] == (int)IsolatedArabicLangLetters.AlefHamza
				|| letters[index - 1] == (int)IsolatedArabicLangLetters.Hamza
				|| letters[index - 1] == (int)IsolatedArabicLangLetters.AlefMaksoor
				|| letters[index - 1] == (int)IsolatedArabicLangLetters.WawHamza;

		bool lettersThatCannotBeALeadingLetter = letters[index] != ' '
			&& letters[index] != (int)IsolatedArabicLangLetters.Dal
			&& letters[index] != (int)IsolatedArabicLangLetters.Thal
				&& letters[index] != (int)IsolatedArabicLangLetters.Ra2
				&& letters[index] != (int)IsolatedArabicLangLetters.Zeen
				&& letters[index] != (int)IsolatedArabicLangLetters.PersianZe
				&& letters[index] != (int)IsolatedArabicLangLetters.Alef
				&& letters[index] != (int)IsolatedArabicLangLetters.AlefHamza
				&& letters[index] != (int)IsolatedArabicLangLetters.AlefMaksoor
				&& letters[index] != (int)IsolatedArabicLangLetters.AlefMad
				&& letters[index] != (int)IsolatedArabicLangLetters.WawHamza
				&& letters[index] != (int)IsolatedArabicLangLetters.Waw
				&& letters[index] != (int)IsolatedArabicLangLetters.Hamza;

		bool lettersThatCannotBeAfterLeadingLetter = index < letters.Length - 1
				&& letters[index + 1] != ' '
				&& letters[index + 1] != '\n'
				&& letters[index + 1] != '\r'
				&& !char.IsPunctuation(letters[index + 1])
				&& !char.IsNumber(letters[index + 1])
				&& !char.IsSymbol(letters[index + 1])
				&& !char.IsLower(letters[index + 1])
				&& !char.IsUpper(letters[index + 1])
				&& letters[index + 1] != (int)IsolatedArabicLangLetters.Hamza;

		if (lettersThatCannotBeBeforeALeadingLetter && lettersThatCannotBeALeadingLetter && lettersThatCannotBeAfterLeadingLetter)

		//		if ((index == 0 || letters[index - 1] == ' ' || letters[index - 1] == '*' || letters[index - 1] == 'A' || char.IsPunctuation(letters[index - 1])
		//		     || letters[index - 1] == '>' || letters[index - 1] == '<' 
		//		     || letters[index - 1] == (int)IsolatedArabicLetters.Alef
		//		     || letters[index - 1] == (int)IsolatedArabicLetters.Dal || letters[index - 1] == (int)IsolatedArabicLetters.Thal
		//		     || letters[index - 1] == (int)IsolatedArabicLetters.Ra2 
		//		     || letters[index - 1] == (int)IsolatedArabicLetters.Zeen || letters[index - 1] == (int)IsolatedArabicLetters.PersianZe
		//		     || letters[index - 1] == (int)IsolatedArabicLetters.AlefMaksora || letters[index - 1] == (int)IsolatedArabicLetters.Waw
		//		     || letters[index - 1] == (int)IsolatedArabicLetters.AlefMad || letters[index - 1] == (int)IsolatedArabicLetters.AlefHamza
		//		     || letters[index - 1] == (int)IsolatedArabicLetters.AlefMaksoor || letters[index - 1] == (int)IsolatedArabicLetters.WawHamza) 
		//		    && letters[index] != ' ' && letters[index] != (int)IsolatedArabicLetters.Dal
		//		    && letters[index] != (int)IsolatedArabicLetters.Thal
		//		    && letters[index] != (int)IsolatedArabicLetters.Ra2 
		//		    && letters[index] != (int)IsolatedArabicLetters.Zeen && letters[index] != (int)IsolatedArabicLetters.PersianZe
		//		    && letters[index] != (int)IsolatedArabicLetters.Alef && letters[index] != (int)IsolatedArabicLetters.AlefHamza
		//		    && letters[index] != (int)IsolatedArabicLetters.AlefMaksoor
		//		    && letters[index] != (int)IsolatedArabicLetters.AlefMad
		//		    && letters[index] != (int)IsolatedArabicLetters.WawHamza
		//		    && letters[index] != (int)IsolatedArabicLetters.Waw
		//		    && letters[index] != (int)IsolatedArabicLetters.Hamza
		//		    && index < letters.Length - 1 && letters[index + 1] != ' ' && !char.IsPunctuation(letters[index + 1] ) && !char.IsNumber(letters[index + 1])
		//		    && letters[index + 1] != (int)IsolatedArabicLetters.Hamza )
		{
			return true;
		}
		else
			return false;
	}

	/// <summary>
	/// Checks if the letter at index value is a finishing character in Arabic or not.
	/// </summary>
	/// <param name="letters">The whole word that contains the character to be checked</param>
	/// <param name="index">The index of the character to be checked</param>
	/// <returns>True if the character at index is a finishing character, else, returns false</returns>
	internal static bool IsFinishingLetter(char[] letters, int index)
	{
		bool indexZero = index != 0;
		bool lettersThatCannotBeBeforeAFinishingLetter = (index == 0) ? false :
				letters[index - 1] != ' '
				//				&& char.IsDigit(letters[index-1])
				//				&& char.IsLower(letters[index-1])
				//				&& char.IsUpper(letters[index-1])
				//				&& char.IsNumber(letters[index-1])
				//				&& char.IsWhiteSpace(letters[index-1])
				//				&& char.IsPunctuation(letters[index-1])
				//				&& char.IsSymbol(letters[index-1])

				&& letters[index - 1] != (int)IsolatedArabicLangLetters.Dal
				&& letters[index - 1] != (int)IsolatedArabicLangLetters.Thal
				&& letters[index - 1] != (int)IsolatedArabicLangLetters.Ra2
				&& letters[index - 1] != (int)IsolatedArabicLangLetters.Zeen
				&& letters[index - 1] != (int)IsolatedArabicLangLetters.PersianZe
				//&& letters[index - 1] != (int)IsolatedArabicLangLetters.AlefMaksora 
				&& letters[index - 1] != (int)IsolatedArabicLangLetters.Waw
				&& letters[index - 1] != (int)IsolatedArabicLangLetters.Alef
				&& letters[index - 1] != (int)IsolatedArabicLangLetters.AlefMad
				&& letters[index - 1] != (int)IsolatedArabicLangLetters.AlefHamza
				&& letters[index - 1] != (int)IsolatedArabicLangLetters.AlefMaksoor
				&& letters[index - 1] != (int)IsolatedArabicLangLetters.WawHamza
				&& letters[index - 1] != (int)IsolatedArabicLangLetters.Hamza



				&& !char.IsPunctuation(letters[index - 1])
				&& !char.IsSymbol(letters[index - 1])
				&& letters[index - 1] != '>'
				&& letters[index - 1] != '<';


		bool lettersThatCannotBeFinishingLetters = letters[index] != ' ' && letters[index] != (int)IsolatedArabicLangLetters.Hamza;




		if (lettersThatCannotBeBeforeAFinishingLetter && lettersThatCannotBeFinishingLetters)

		//		if (index != 0 && letters[index - 1] != ' ' && letters[index - 1] != '*' && letters[index - 1] != 'A'
		//		    && letters[index - 1] != (int)IsolatedArabicLetters.Dal && letters[index - 1] != (int)IsolatedArabicLetters.Thal
		//		    && letters[index - 1] != (int)IsolatedArabicLetters.Ra2 
		//		    && letters[index - 1] != (int)IsolatedArabicLetters.Zeen && letters[index - 1] != (int)IsolatedArabicLetters.PersianZe
		//		    && letters[index - 1] != (int)IsolatedArabicLetters.AlefMaksora && letters[index - 1] != (int)IsolatedArabicLetters.Waw
		//		    && letters[index - 1] != (int)IsolatedArabicLetters.Alef && letters[index - 1] != (int)IsolatedArabicLetters.AlefMad
		//		    && letters[index - 1] != (int)IsolatedArabicLetters.AlefHamza && letters[index - 1] != (int)IsolatedArabicLetters.AlefMaksoor
		//		    && letters[index - 1] != (int)IsolatedArabicLetters.WawHamza && letters[index - 1] != (int)IsolatedArabicLetters.Hamza 
		//		    && !char.IsPunctuation(letters[index - 1]) && letters[index - 1] != '>' && letters[index - 1] != '<' 
		//		    && letters[index] != ' ' && index < letters.Length
		//		    && letters[index] != (int)IsolatedArabicLetters.Hamza)
		{
			//try
			//{
			//    if (char.IsPunctuation(letters[index + 1]))
			//        return true;
			//    else
			//        return false;
			//}
			//catch (Exception e)
			//{
			//    return false;
			//}

			return true;
		}
		//return true;
		else
			return false;
	}

	/// <summary>
	/// Checks if the letter at index value is a middle character in Arabic or not.
	/// </summary>
	/// <param name="letters">The whole word that contains the character to be checked</param>
	/// <param name="index">The index of the character to be checked</param>
	/// <returns>True if the character at index is a middle character, else, returns false</returns>
	internal static bool IsMiddleLetter(char[] letters, int index)
	{
		bool lettersThatCannotBeMiddleLetters = (index == 0) ? false :
			letters[index] != (int)IsolatedArabicLangLetters.Alef
				&& letters[index] != (int)IsolatedArabicLangLetters.Dal
				&& letters[index] != (int)IsolatedArabicLangLetters.Thal
				&& letters[index] != (int)IsolatedArabicLangLetters.Ra2
				&& letters[index] != (int)IsolatedArabicLangLetters.Zeen
				&& letters[index] != (int)IsolatedArabicLangLetters.PersianZe
				//&& letters[index] != (int)IsolatedArabicLangLetters.AlefMaksora
				&& letters[index] != (int)IsolatedArabicLangLetters.Waw
				&& letters[index] != (int)IsolatedArabicLangLetters.AlefMad
				&& letters[index] != (int)IsolatedArabicLangLetters.AlefHamza
				&& letters[index] != (int)IsolatedArabicLangLetters.AlefMaksoor
				&& letters[index] != (int)IsolatedArabicLangLetters.WawHamza
				&& letters[index] != (int)IsolatedArabicLangLetters.Hamza;

		bool lettersThatCannotBeBeforeMiddleCharacters = (index == 0) ? false :
				letters[index - 1] != (int)IsolatedArabicLangLetters.Alef
				&& letters[index - 1] != (int)IsolatedArabicLangLetters.Dal
				&& letters[index - 1] != (int)IsolatedArabicLangLetters.Thal
				&& letters[index - 1] != (int)IsolatedArabicLangLetters.Ra2
				&& letters[index - 1] != (int)IsolatedArabicLangLetters.Zeen
				&& letters[index - 1] != (int)IsolatedArabicLangLetters.PersianZe
				//&& letters[index - 1] != (int)IsolatedArabicLangLetters.AlefMaksora
				&& letters[index - 1] != (int)IsolatedArabicLangLetters.Waw
				&& letters[index - 1] != (int)IsolatedArabicLangLetters.AlefMad
				&& letters[index - 1] != (int)IsolatedArabicLangLetters.AlefHamza
				&& letters[index - 1] != (int)IsolatedArabicLangLetters.AlefMaksoor
				&& letters[index - 1] != (int)IsolatedArabicLangLetters.WawHamza
				&& letters[index - 1] != (int)IsolatedArabicLangLetters.Hamza
				&& !char.IsPunctuation(letters[index - 1])
				&& letters[index - 1] != '>'
				&& letters[index - 1] != '<'
				&& letters[index - 1] != ' '
				&& letters[index - 1] != '*';

		bool lettersThatCannotBeAfterMiddleCharacters = (index >= letters.Length - 1) ? false :
			letters[index + 1] != ' '
				&& letters[index + 1] != '\r'
				&& letters[index + 1] != (int)IsolatedArabicLangLetters.Hamza
				&& !char.IsNumber(letters[index + 1])
				&& !char.IsSymbol(letters[index + 1])
				&& !char.IsPunctuation(letters[index + 1]);
		if (lettersThatCannotBeAfterMiddleCharacters && lettersThatCannotBeBeforeMiddleCharacters && lettersThatCannotBeMiddleLetters)

		//		if (index != 0 && letters[index] != ' '
		//		    && letters[index] != (int)IsolatedArabicLangLetters.Alef && letters[index] != (int)IsolatedArabicLetters.Dal
		//		    && letters[index] != (int)IsolatedArabicLetters.Thal && letters[index] != (int)IsolatedArabicLetters.Ra2
		//		    && letters[index] != (int)IsolatedArabicLetters.Zeen && letters[index] != (int)IsolatedArabicLetters.PersianZe 
		//		    && letters[index] != (int)IsolatedArabicLetters.AlefMaksora
		//		    && letters[index] != (int)IsolatedArabicLetters.Waw && letters[index] != (int)IsolatedArabicLetters.AlefMad
		//		    && letters[index] != (int)IsolatedArabicLetters.AlefHamza && letters[index] != (int)IsolatedArabicLetters.AlefMaksoor
		//		    && letters[index] != (int)IsolatedArabicLetters.WawHamza && letters[index] != (int)IsolatedArabicLetters.Hamza
		//		    && letters[index - 1] != (int)IsolatedArabicLetters.Alef && letters[index - 1] != (int)IsolatedArabicLetters.Dal
		//		    && letters[index - 1] != (int)IsolatedArabicLetters.Thal && letters[index - 1] != (int)IsolatedArabicLetters.Ra2
		//		    && letters[index - 1] != (int)IsolatedArabicLetters.Zeen && letters[index - 1] != (int)IsolatedArabicLetters.PersianZe 
		//		    && letters[index - 1] != (int)IsolatedArabicLetters.AlefMaksora
		//		    && letters[index - 1] != (int)IsolatedArabicLetters.Waw && letters[index - 1] != (int)IsolatedArabicLetters.AlefMad
		//		    && letters[index - 1] != (int)IsolatedArabicLetters.AlefHamza && letters[index - 1] != (int)IsolatedArabicLetters.AlefMaksoor
		//		    && letters[index - 1] != (int)IsolatedArabicLetters.WawHamza && letters[index - 1] != (int)IsolatedArabicLetters.Hamza 
		//		    && letters[index - 1] != '>' && letters[index - 1] != '<' 
		//		    && letters[index - 1] != ' ' && letters[index - 1] != '*' && !char.IsPunctuation(letters[index - 1])
		//		    && index < letters.Length - 1 && letters[index + 1] != ' ' && letters[index + 1] != '\r' && letters[index + 1] != 'A' 
		//		    && letters[index + 1] != '>' && letters[index + 1] != '>' && letters[index + 1] != (int)IsolatedArabicLetters.Hamza
		//		    )
		{
			try
			{
				if (char.IsPunctuation(letters[index + 1]))
					return false;
				else
					return true;
			}
			catch
			{
				return false;
			}
			//return true;
		}
		else
			return false;
	}

}