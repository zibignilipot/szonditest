using System.Collections.Generic;
using SzondiTest;

namespace SzondiTestUnitTests
{
	internal static class Fälle
	{
		private static List<TestProfile> fall16;
		private static List<TestProfile> fall17;
		private static List<TestProfile> fall18;
		private static List<TestProfile> fall19;
		private static List<TestProfile> fall20;
		private static List<TestProfile> fall21;
		private static List<TestProfile> fall22;
		private static List<TestProfile> fall23;
		private static List<TestProfile> fall24;
		private static List<TestProfile> fall25;
		private static List<TestProfile> fall26;
		private static List<TestProfile> fall27;
		
		public static List<TestProfile> Fall16
		{
			get
			{
				if(fall16==null)
				{
					fall16 = new List<TestProfile>()
					{
						null,
						new TestProfile("+,+", "±,-", "0,-", "+,±"),
						new TestProfile("+!!,-", "0,-", "0,-!", "+,0"),
						new TestProfile("+!,+", "-,-!", "0,-", "+,±"),
						new TestProfile("+,±", "0,-!", "0,-", "+,-"),
						new TestProfile("+,±", "0,-", "0,-", "+,-"),
						new TestProfile("+,±", "+,-", "0,-", "+,-"),
						new TestProfile("+!,±", "0,-", "0,-", "+,-"),
						null,// 8==6
						new TestProfile("+,±", "0,-", "0,-", "+!,-"),
						new TestProfile("+,-", "+,-", "0,-", "+!,-"),
					};
					BaseSzondiUnitTests.SetSexAndNameForProfiles(Sex.Male, "Fall 16", fall16);
				}
				return fall16;
			}
		}	

		public static List<TestProfile> Fall17
		{
			get
			{
				if(fall17==null)
				{
					fall17 = new List<TestProfile>()
					{
						null,
						new TestProfile("0,+", "±,0", "-,-", "+!,±"),
						new TestProfile("+,-", "+,0", "-,-", "+,±"),
						new TestProfile("+,-", "+,-", "0,-", "+,+!"),
						new TestProfile("+,-", "+,-", "0,-", "+,-"),
						new TestProfile("+,-", "+,-", "0,-", "+!,-"),//5
						
						new TestProfile("0,-", "+,0", "0,-", "+!!,±"),//6
						new TestProfile("+,-", "+,0", "-,-", "+!,-"),
						new TestProfile("+,-", "+,-", "0,-", "+,±"),
						new TestProfile("+,-", "+,-", "0,-", "+!,+"),
						new TestProfile("+,-", "+,-", "-,-", "+!,±"),
					};
					BaseSzondiUnitTests.SetSexAndNameForProfiles(Sex.Female, "Fall 17", fall17);
				}
				
				return fall17;
			}
		}	

		public static List<TestProfile> Fall18
		{
			get
			{
				if(fall18==null)
				{
					fall18 = new List<TestProfile>()
					{
						null,
						new TestProfile("+,+", "0,-", "0,-", "+,-"),
						new TestProfile("+!,0", "+,-", "-,±", "0,-"),
						new TestProfile("+!,+", "±,-", "-,-", "0,-"),
						new TestProfile("+!!,0", "+,-", "0,-", "0,-!"),
						new TestProfile("+!!,0", "±,-", "0,-", "0,-"),//5
						
						new TestProfile("+!!,0", "0,-", "0,-", "+,-"),//6
						new TestProfile("+!!,-", "+,-", "0,-", "0,-"),
						new TestProfile("+!!,-", "+,-", "0,-", "0,-"),
						new TestProfile("+!!,0", "+,-", "0,-", "0,-"),
						new TestProfile("+!!,0", "+,0", "0,-!", "0,-"),
					};
					BaseSzondiUnitTests.SetSexAndNameForProfiles(Sex.Female, "Fall 18", fall18);
				}
				
				return fall18;
			}
		}
		
		public static List<TestProfile> Fall19
		{
			get
			{
				if(fall19==null)
				{
					fall19 = new List<TestProfile>()
					{
						null,
						new TestProfile("+,0", "0,±", "±,-", "+!,-"),
						new TestProfile("+,0", "±,-", "+,-!", "+,0"),
						new TestProfile("+,-", "-,+", "0,-", "+!,0"),
						new TestProfile("+!,-", "0,±", "0,-!", "+,±"),
						new TestProfile("±,-", "0,0", "+,-", "+!!,-"),//5
						
						new TestProfile("±,-", "0,0", "0,-", "+!,-"),//6
						new TestProfile("+,-", "±,-", "+,0", "+,-"),
						new TestProfile("+,-!", "±,+", "0,0", "+,-"),
						new TestProfile("+,-!", "0,±", "0,0", "+!,-"),
						new TestProfile("±,-", "±,0", "0,-", "!±,0"),
					};
					BaseSzondiUnitTests.SetSexAndNameForProfiles(Sex.Female, "Fall 19", fall19);
				}
				
				return fall19;
			}
		}
		
		public static List<TestProfile> Fall20
		{
			get
			{
				if(fall20==null)
				{
					fall20 = new List<TestProfile>()
					{
						null,
						new TestProfile("+,-", "+,±", "±,-", "+,-"),
						new TestProfile("+,0", "±,-", "-,-", "+,-"),
						new TestProfile("+,-", "0,-", "±,+", "+,-"),
						new TestProfile("+,0", "0,-", "±,-", "+!,-"),
						new TestProfile("+!,-", "+,-", "±,-", "+,-"),//5
						
						new TestProfile("+,0", "+,-", "-,-", "+,0"),//6
						new TestProfile("+!!,+", "0,-", "±!,-", "+,-"),
						new TestProfile("+!!,0", "+,0", "-!,-", "+,-!"),
						new TestProfile("+!,0", "0,±", "±!,-", "+,0"),
						new TestProfile("+!,0", "0,+", "±,-", "+,-"),
					};
					BaseSzondiUnitTests.SetSexAndNameForProfiles(Sex.Male, "Fall 20", fall20);
				}
				
				return fall20;
			}
		}
		
		public static List<TestProfile> Fall21
		{
			get
			{
				if(fall21==null)
				{
					fall21 = new List<TestProfile>()
					{
						null,
						null,null,null,null,null,null,null,null,null,null,
						new TestProfile("-,±", "-,±", "0,+!", "-,+"),//11
						new TestProfile("-,±", "-,+!", "0,+", "-,0"),
						new TestProfile("-,-!", "-,+", "0,+!", "0,+"),
						new TestProfile("±,±", "-,0", "0,+", "0,+"),
						new TestProfile("-,-", "±,0", "0,+", "-,±"),//15
						
						new TestProfile("-,-", "+,0", "0,+", "-,±"),//16
						new TestProfile("-,±", "±,0", "0,+", "-,+"),
						new TestProfile("-,±", "0,+", "0,+!", "0,-"),
						new TestProfile("±,±", "±,0", "0,+", "0,±"),
						new TestProfile("±,-", "+,+", "0,+", "-,±"),
					};
					BaseSzondiUnitTests.SetSexAndNameForProfiles(Sex.Female, "Fall 21", fall21);
				}
				
				return fall21;
			}
		}
		
		public static List<TestProfile> Fall22
		{
			get
			{
				if(fall22==null)
				{
					fall22 = new List<TestProfile>()
					{
						null,
						new TestProfile("+,+", "0,-", "-,0", "±,-"),
						new TestProfile("±,+!!", "-,-", "0,+", "-,-"),
						new TestProfile("+,+!", "-,+", "-,0", "0,-"),
						new TestProfile("!±,+!", "-,0", "-,0", "-,-"),
						new TestProfile("±,+!!!", "-,-", "-,0", "-,-"),//5
						
						new TestProfile("+,+!", "-,±", "-,+", "-,-"),//6
						new TestProfile("±,+!!", "-,±", "-,0", "-,-"),
						new TestProfile("±,+!!!", "-,±", "-,0", "-,-"),
						new TestProfile("0,+!!!", "0,-", "-,+", "-,-"),
						new TestProfile("0,+!!", "0,±", "-,+", "-,-!"),
					};
					BaseSzondiUnitTests.SetSexAndNameForProfiles(Sex.Male, "Fall 22", fall22);
				}
				
				return fall22;
			}
		}
		
		public static List<TestProfile> Fall23
		{
			get
			{
				if(fall23==null)
				{
					fall23 = new List<TestProfile>()
					{
						null,
						new TestProfile("+!!!,+!", "-,-", "-,0", "-,-"),
						new TestProfile("+!!!,+!", "-,-", "-,0", "-,-"),
						new TestProfile("+!!!,+!", "+,-", "-,-", "-,0"),
						new TestProfile("+!!,+!!", "±,-", "-,0", "-,0"),
						new TestProfile("+!!!,+!", "-,-", "-,0", "-,-"),//5
						
						new TestProfile("+!!!,+!", "±,-", "-,0", "-,-"),//6
						new TestProfile("+!!,+!!", "-,-", "-,0", "-,-"),
						new TestProfile("+!!,+!!", "-,-", "-,-", "-,-"),
						new TestProfile("+!!,+!!", "-,-", "-,0", "-,-"),
						new TestProfile("+!!,+!!", "-,-", "-,0", "-,-"),
					};
					BaseSzondiUnitTests.SetSexAndNameForProfiles(Sex.Male, "Fall 23", fall23);
				}
				
				return fall23;
			}
		}
		
		public static List<TestProfile> Fall24
		{
			get
			{
				if(fall24==null)
				{
					fall24 = new List<TestProfile>()
					{
						null,
						new TestProfile("+!!!,+!!!", "0,-!", "-,-", "0,0"),
						new TestProfile("+!!,+!", "0,-!!", "0,-", "0,0"),
						new TestProfile("+!!!,+!!", "0,-!", "-!,-", "0,0"),
						new TestProfile("+!!!,+!!", "0,-", "-!,-", "0,-"),
						new TestProfile("+!!,+!!!", "0,-", "-!,-", "0,0"),//5
						
						new TestProfile("+!!!,+!!!", "0,-!", "-,-", "0,-"),//6
						new TestProfile("+!!,+!!", "0,-", "-,-", "0,0"),
						new TestProfile("+!,+!!", "0,-!", "-,+", "0,0"),
						new TestProfile("+!!,+!!!", "0,-", "-!!,0", "0,-"),
						new TestProfile("+!!,+!!!", "0,-!", "-!,0", "0,0"),
					};
					BaseSzondiUnitTests.SetSexAndNameForProfiles(Sex.Male, "Fall 24", fall24);
				}
				
				return fall24;
			}
		}
		
		public static List<TestProfile> Fall25
		{
			get
			{
				if(fall25==null)
				{
					fall25 = new List<TestProfile>()
					{
						null,
						new TestProfile("±,+!", "0,-!", "0,±", "-,0"),
						new TestProfile("+,+!!", "0,±", "0,±", "-,-"),
						new TestProfile("!±,+!!", "0,-", "0,±", "0,-"),
						new TestProfile("!±,+!!", "0,-", "0,±", "-,-"),
						new TestProfile("±,+!!", "0,±", "0,±", "0,-"),//5
						
						new TestProfile("±,+!!", "0,±", "0,±", "0,-"),//6
						new TestProfile("-!,+", "0,+!", "-,-", "0,+"),
						new TestProfile("±,+!!", "0,+", "-,0", "-,-"),
						new TestProfile("-,+!!!", "0,-", "-,±", "0,-"),
						new TestProfile("-,+!!!", "0,-", "0,±", "0,-"),
					};
					BaseSzondiUnitTests.SetSexAndNameForProfiles(Sex.Male, "Fall 25", fall25);
				}
				
				return fall25;
			}
		}
		
		public static List<TestProfile> Fall26
		{
			get
			{
				if(fall26==null)
				{
					fall26 = new List<TestProfile>()
					{
						null,
						new TestProfile("+,+!!", "0,-", "-!,-", "-,0"),
						new TestProfile("+!,+!", "0,-", "-,-", "0,-"),
						new TestProfile("+!,+!", "0,-", "-,±!", "0,0"),
						new TestProfile("+!,+!!!", "0,-", "-,-", "-,0"),
						new TestProfile("+!,+!", "-,-", "-,+", "0,-"),//5
						
						new TestProfile("+!!!,+!", "-,-", "-,0", "0,-"),//6
						new TestProfile("+!,+!!!", "-,-", "-,±", "-,0"),
						new TestProfile("+!,+!!!", "-,-!", "-,±", "0,-"),
						new TestProfile("+!!!,+!!!", "-,-", "-,-", "0,-"),
						new TestProfile("+!!!,+!!", "0,-", "-,-", "-,-"),
					};
					BaseSzondiUnitTests.SetSexAndNameForProfiles(Sex.Female, "Fall 26", fall26);
				}
				
				return fall26;
			}
		}
		
		public static List<TestProfile> Fall27
		{
			get
			{
				if(fall27==null)
				{
					fall27 = new List<TestProfile>()
					{
						null,
						new TestProfile("0,-", "+,-", "-,+", "-,+"),
						new TestProfile("+,+", "+!,-", "-,0", "-,+"),
						new TestProfile("-!,0", "+!,-", "0,+!", "-,+"),
						new TestProfile("0,-", "+,-!", "+!,-", "0,+"),
						new TestProfile("0,0", "+,-!", "±,+", "-,+"),//5
						
						new TestProfile("-,0", "+,-", "+,+", "-,±"),//6
						new TestProfile("-,-", "+!,-", "±,+", "-,+"),
						new TestProfile("-,-", "+,-", "+,+", "-,+!"),
					};
					BaseSzondiUnitTests.SetSexAndNameForProfiles(Sex.Female, "Fall 27", fall27);
				}
				
				return fall27;
			}
		}
	}
}