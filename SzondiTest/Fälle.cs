﻿using System.Collections.Generic;
using SzondiTest;

namespace SzondiTestUnitTests
{
	internal static class Fälle
	{
		private static List<TestProfile> fall1;
		private static List<TestProfile> fall2;
		// 3,4
		private static List<TestProfile> fall5;
		private static List<TestProfile> fall6;
		private static List<TestProfile> fall7;
		//8-11
		private static List<TestProfile> fall12;
		//13
		//14
		//15
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
		// 28
		// 29
		private static List<TestProfile> fall30;
		private static List<TestProfile> fall31;
		private static List<TestProfile> fall32;
		private static List<TestProfile> fall33;
		private static List<TestProfile> fall34;
		private static List<TestProfile> fall35;
		private static List<TestProfile> fall36;
		private static List<TestProfile> fall37;		
		private static List<TestProfile> fall38;
		private static List<TestProfile> fall39;
		private static List<TestProfile> fall40;
		private static List<TestProfile> fall42;
		
		private static List<TestProfile> b3Tab49Mann;
		private static List<TestProfile> b3Tab49Frau;
		private static List<TestProfile> b3Tab50I;
		private static List<TestProfile> b3Tab50II;
		private static List<TestProfile> b3Tab50III;
		private static List<TestProfile> b3Tab50IV;
		private static List<TestProfile> b2BiExi1;
		private static List<TestProfile> b2BiExi2;
		private static List<TestProfile> b2Abb40;
		
		public static List<TestProfile> Fall01
		{
			get
			{
				if(fall1==null)
				{
					fall1 = new List<TestProfile>()
					{
						null,
						new TestProfile("±,0", "0,-", "-,+", "+,±"),//1
						new TestProfile("+,±", "0,0", "-,+", "+,-"),//2
						new TestProfile("+,±", "0,-", "0,0", "+!,-"),//3
						new TestProfile("+,±", "0,0", "-,0", "+!,±"),//4
						new TestProfile("+,±", "0,±", "-,0", "+,-"),//5
						
						new TestProfile("±,±", "0,-", "-,0", "+!,-"),//6
						new TestProfile("+,-!!", "0,0", "0,0", "+!,±"),//7
						new TestProfile("-,-", "0,0", "-,+", "+!!,-"),//8
						new TestProfile("+,±", "0,-", "-,0", "+!,±"),//9
						new TestProfile("+,±", "0,-", "0,0", "+!!,-"),//10
					};
					BaseSzondiUnitTests.SetSexAndNameForProfiles(Sex.Male, "Fall 1", fall1);
				}
				return fall1;
			}
		}	
		
		public static List<TestProfile> Fall02
		{
			get
			{
				if(fall2==null)
				{
					fall2 = new List<TestProfile>()
					{
						null,
						new TestProfile("±,+", "0,±", "0,±", "0,0"),
						new TestProfile("±,+", "±,-", "0,±", "0,0"),
						new TestProfile("±,0", "-,0", "0,+", "0,±"),
						new TestProfile("±,+", "±,0", "-,+", "0,-"),
						new TestProfile("±,+", "±,0", "0,±", "0,-"),//5
						
						new TestProfile("±,+", "±,0", "0,±", "0,-"),//6
						new TestProfile("±,+", "±,+", "0,±", "0,-"),
						new TestProfile("±,+", "±,+", "0,±", "0,-"),
						new TestProfile("±,+", "±,0", "0,±", "0,-"),
						new TestProfile("±,+", "±,0", "0,±", "0,-"),
					};
					BaseSzondiUnitTests.SetSexAndNameForProfiles(Sex.Male, "Fall 2", fall2);
				}
				return fall2;
			}
		}	
		
		public static List<TestProfile> Fall05
		{
			get
			{
				if(fall5==null)
				{
					fall5 = new List<TestProfile>()
					{
						null,
						new TestProfile("+!,-", "+,-", "0,-", "+,0"),
						new TestProfile("+,-", "0,-", "0,±", "+!,+"),
						new TestProfile("0,-!", "+,-", "0,±", "+,0"),
						new TestProfile("+,-", "0,-", "-,±", "+,±"),
						new TestProfile("+,-!", "0,-", "-,-", "+,+"),//5
						
						new TestProfile("+,0", "0,-", "-,±", "+,±"),//6
						new TestProfile("+,-", "0,-", "-,-", "+!,+"),
						new TestProfile("+,-", "0,-", "-,-", "+!,+"),//8
						new TestProfile("+,-", "0,-", "-,-", "+!,+"),
						new TestProfile("0,±", "0,-", "-,-", "+,+"),
					};
					BaseSzondiUnitTests.SetSexAndNameForProfiles(Sex.Male, "Fall 5", fall5);
				}
				return fall5;
			}
		}	
		
		public static List<TestProfile> Fall06
		{
			get
			{
				if(fall6==null)
				{
					fall6 = new List<TestProfile>()
					{
						null,
						new TestProfile("-,-!!", "0,+", "0,±", "+!,+"),
						new TestProfile("±,-!", "0,+", "0,±", "+,+"),
						new TestProfile("0,-!!", "0,±", "-,0", "+,+"),
						new TestProfile("±,-", "0,±", "-,0", "+,+"),
						new TestProfile("0,-!", "0,±", "0,-", "+,+"),//5
						
						new TestProfile("±,-", "0,±", "+,-", "+!,-"),//6
						new TestProfile("±,-", "0,±", "0,±", "+!,+"),
						new TestProfile("±,-!!", "0,±", "0,±", "+,0"),//8
						new TestProfile("+,-!", "0,±", "0,±", "+!,+"),
						new TestProfile("±,-!!", "+,+", "0,-", "+,+"),
					};
					BaseSzondiUnitTests.SetSexAndNameForProfiles(Sex.Male, "Fall 6", fall6);
				}
				return fall6;
			}
		}
		
		public static List<TestProfile> Fall07
		{
			get
			{
				if(fall7==null)
				{
					fall7 = new List<TestProfile>()
					{
						null,
						new TestProfile("-,-", "0,+", "+,0", "+,-"),
						new TestProfile("-,-", "0,+!", "0,+", "+,0"),
						new TestProfile("-!!,-!!", "0,+", "+,0", "+,0"),
						new TestProfile("-!!,-!", "+,+", "+,0", "+,0"),
						new TestProfile("-!,±", "0,+", "+,0", "+,-"),//5
						
						new TestProfile("-!,-!", "0,+", "0,0", "+!!,-"),//6
						new TestProfile("-!!,-", "+,+", "+,0", "+!,0"),
						new TestProfile("-!,-!", "+,+", "+,0", "+!,-"),//8
						new TestProfile("-!!,-", "+,+", "0,+", "+,-"),
						new TestProfile("-!,-", "0,+", "+,0", "+!,-"),
					};
					BaseSzondiUnitTests.SetSexAndNameForProfiles(Sex.Female, "Fall 7", fall7);
				}
				return fall7;
			}
		}
		
		public static List<TestProfile> Fall12
		{
			get
			{
				if(fall12==null)
				{
					fall12 = new List<TestProfile>()
					{
						null,
						new TestProfile("-,-!", "+,0", "+,+", "±,0"),
					};
					
					var fall12Complementar = new List<TestProfile>()
					{
						null,
						new TestProfile("-,-", "+,±", "+,+", "0,±"),
					};
					
					BaseSzondiUnitTests.SetSexAndNameForProfiles(Sex.Male, 
					                                             "Fall 12", 
					                                             fall12,
					                                             fall12Complementar);
				}
				return fall12;
			}
		}	
		
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
						new TestProfile("+,±", "0,-", "0,-", "+,-"),//5
						
						new TestProfile("+,±", "+,-", "0,-", "+,-"),//6
						new TestProfile("+!,±", "0,-", "0,-", "+,-"),
						new TestProfile("+,±", "+,-", "0,-", "+,-"),// 8==6
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
		
		public static List<TestProfile> Fall30
		{
			get
			{
				if(fall30==null)
				{
					fall30 = new List<TestProfile>()
					{
						null,
						new TestProfile("0,0", "-!,+", "+,-", "+!,-"),
						new TestProfile("+,-", "-!,0", "+!!,0", "+,+"),
						new TestProfile("0,+", "+,±", "±,0", "±,±"),
						new TestProfile("+,-", "-,+", "+,0", "+,±"),
						new TestProfile("+,-", "-,+", "0,-!", "+,±"),//5
						
						new TestProfile("+,0", "0,0", "+,-", "+!,-!!"),//6
						new TestProfile("+,-", "0,-", "+!,-", "+,±"),
						new TestProfile("+,0", "-,-", "+,-", "0,-"),
						new TestProfile("+!,0", "-,-", "±,-", "+,±"),
						new TestProfile("+,-", "0,0", "±,-", "+!,-"),
					};
					BaseSzondiUnitTests.SetSexAndNameForProfiles(Sex.Female, "Fall 30", fall30);
				}
				
				return fall30;
			}
		}
		
		public static List<TestProfile> Fall31
		{
			get
			{
				if(fall31==null)
				{
					fall31 = new List<TestProfile>()
					{
						null,
						new TestProfile("+!,+!", "+,-", "-,-", "0,-"),
						new TestProfile("+!,+", "+,-", "-,±", "-,-"),
						new TestProfile("+!,+!", "±,0", "-!,-", "-,0"),
						new TestProfile("+!,+!!", "0,0", "-!,±", "-,-"),
						new TestProfile("+,+!", "0,-", "-,+", "0,-!!"),//5
						
						new TestProfile("+!,+!", "+,-", "-!,-", "0,-"),//6
						new TestProfile("+!,+!!!", "0,+", "-,-", "0,-"),
						new TestProfile("+!,+!!!", "0,0", "-!,-", "0,-"),
						new TestProfile("+,+!", "+,-", "-,-", "-,-!"),
						new TestProfile("+!,+!", "0,-", "0,-", "0,-"),
					};
					BaseSzondiUnitTests.SetSexAndNameForProfiles(Sex.Female, "Fall 31", fall31);
				}
				
				return fall31;
			}
		}
		
		public static List<TestProfile> Fall32
		{
			get
			{
				if(fall32==null)
				{
					fall32 = new List<TestProfile>()
					{
						null,
						new TestProfile("+,-!!!", "+,±", "+,0", "0,+"),
						new TestProfile("+,-!!", "+,±", "+,0", "0,+"),//II
						new TestProfile("+,-!", "-,±", "+!,0", "-,+"),//3
						new TestProfile("+,-!", "±,±", "+,0", "0,+"),//IV
						new TestProfile("+!,-!!!", "+,±", "+,0", "0,+"),//5
						
						new TestProfile("+!,-!!", "±,±", "+,0", "0,+"),
					};
					BaseSzondiUnitTests.SetSexAndNameForProfiles(Sex.Male, "Fall 32", fall32);
				}
				
				return fall32;
			}
		}
		
		public static List<TestProfile> Fall33
		{
			get
			{
				if(fall33==null)
				{
					fall33 = new List<TestProfile>()
					{
						null,
						new TestProfile("+,-!", "-,-", "+,+", "+,±"),
						new TestProfile("+!,-!", "+,-", "0,+", "±,-"),//
						new TestProfile("+,-", "+,-", "±,-", "+,±"),//
						new TestProfile("+,-", "-,-", "±,+", "+,±"),//IV
						new TestProfile("+,-", "-,-", "+,+", "+,-"),//5
						
						new TestProfile("+,-!", "0,-", "+,0", "!±,-"),//6
						new TestProfile("+,-!!", "0,-", "+,+", "+,-"),
						new TestProfile("+,-!!", "0,±", "0,0", "+,-"),
						new TestProfile("+,-!!", "+,-", "+,±", "+,0"),
						new TestProfile("+,-!", "0,-", "0,-", "+!,+"),
					};
					BaseSzondiUnitTests.SetSexAndNameForProfiles(Sex.Male, "Fall 33", fall33);
				}
				
				return fall33;
			}
		}
		
		public static List<TestProfile> Fall34
		{
			get
			{
				if(fall34==null)
				{
					fall34 = new List<TestProfile>()
					{
						null,
						new TestProfile("±,+", "0,0", "-,±", "0,±"),
						new TestProfile("-,-!!!", "-,+", "0,+!!", "0,+"),
						new TestProfile("+,±", "+,0", "+,-", "0,-!"),
						new TestProfile("±,+", "-,0", "-,+", "+,0"),
						new TestProfile("±,±", "+,-", "0,-", "0,+"),//5
						
						new TestProfile("-,+!!", "-,0", "0,-", "0,+"),//6
						new TestProfile("!±,+", "-,-", "0,+!", "0,-"),
						new TestProfile("+!,+!!!", "-,0", "-,-", "0,0"),
						new TestProfile("±,±", "-,0", "0,+!", "0,+"),//9
						new TestProfile("±,-", "+,-", "+,+!", "0,±"),//10
					};
					BaseSzondiUnitTests.SetSexAndNameForProfiles(Sex.Male, "Fall 34", fall34);
				}
				
				return fall34;
			}
		}
		
		public static List<TestProfile> Fall35
		{
			get
			{
				if(fall35==null)
				{
					fall35 = new List<TestProfile>()
					{
						null,
						new TestProfile("0,-", "0,!±", "+,0", "±,0"),
						new TestProfile("-,-", "0,!±", "+,0", "±,0"),
					};
					
					var fall35Complementar = new List<TestProfile>()
					{
						null,
						new TestProfile("-,-", "±,Ø", "+,+", "0,±"),
						new TestProfile("-,-", "±,Ø", "+,±", "0,-"),
					};
					
					BaseSzondiUnitTests.SetSexAndNameForProfiles(Sex.Male, 
					                                             "Fall 35", 
					                                             fall35,
					                                             fall35Complementar);
				}
				
				return fall35;
			}
		}
		
		public static List<TestProfile> Fall36
		{
			get
			{
				if(fall36==null)
				{
					fall36 = new List<TestProfile>()
					{
						null,
						new TestProfile("+,-", "-,+", "-,+", "+,-"),
						new TestProfile("+,-", "-,+", "-,+!", "-,±"),
						new TestProfile("+!,-", "0,0", "-,±", "+,0"),
						new TestProfile("+!!,+", "-,0", "-,-", "+,-"),
						new TestProfile("+!!,+!", "-,0", "-,-", "0,-"),//5
						
						new TestProfile("+!!,0", "0,-", "-,±", "+,-"),//6
						new TestProfile("+!,+", "0,-", "-,+", "0,-!!"),
						new TestProfile("+!,+", "+,-", "0,±", "0,-"),
						new TestProfile("+!,+", "-,0", "-!!,+", "0,-"),//9
						new TestProfile("+!,+", "0,-", "-,+", "-,-"),//10
					};
					BaseSzondiUnitTests.SetSexAndNameForProfiles(Sex.Male, "Fall 36", fall36);
				}
				
				return fall36;
			}
		}
		
		public static List<TestProfile> Fall37
		{
			get
			{
				if(fall37==null)
				{
					fall37 = new List<TestProfile>()
					{
						null,
						new TestProfile("0,0", "±,-", "-,-", "0,+!!"),
						new TestProfile("+,+", "+,±", "-!,-", "+,0"),
						new TestProfile("0,-", "+,-", "-,±", "+,+"),
						new TestProfile("+,+", "±,-", "0,±", "-,+"),
						new TestProfile("0,±", "+,-", "-!,+", "-,+"),//5
						
						new TestProfile("0,0", "0,-", "-,±", "-,+!!"),//6
					};
					BaseSzondiUnitTests.SetSexAndNameForProfiles(Sex.Male, "Fall 37", fall37);
				}
				
				return fall37;
			}
		}
		
		public static List<TestProfile> Fall38
		{
			get
			{
				if(fall38==null)
				{
					fall38 = new List<TestProfile>()
					{
						null,
						new TestProfile("+!,-!", "+,0", "0,-", "-,±"),
						new TestProfile("-,±", "±,+", "0,0", "-,0"),
						new TestProfile("-!,-", "0,+", "+,±", "0,+"),
						new TestProfile("±,-!", "0,+", "+,±", "+,0"),
						new TestProfile("+!,-!", "+,0", "0,-", "-,±"),//5
						
						new TestProfile("±,-!", "+,+", "0,±", "0,-"),//6
					};
					BaseSzondiUnitTests.SetSexAndNameForProfiles(Sex.Male, "Fall 38", fall38);
				}
				
				return fall38;
			}
		}
		
		public static List<TestProfile> Fall39
		{
			get
			{
				if(fall39==null)
				{
					fall39 = new List<TestProfile>()
					{
						null,
						new TestProfile("±,-", "0,0", "-,+", "+!,±"),
						new TestProfile("+,-", "0,-", "-,+", "+,-"),
						new TestProfile("+,-", "0,-", "-,+", "+!,±"),
						new TestProfile("+,-", "-,0", "0,±", "+,±"),
						new TestProfile("±,-", "+,0", "-,+", "+,±"),//5
						
						new TestProfile("+,-", "0,-", "-,+", "+,±"),//6
						new TestProfile("+!,-", "0,-", "-,+", "+,±"),
						new TestProfile("+,-", "-,-", "-,+", "+,±"),
						new TestProfile("+!,-", "0,-", "-,+", "0,±"),
						new TestProfile("+!,-", "0,-", "0,±", "+,+"),
					};
					BaseSzondiUnitTests.SetSexAndNameForProfiles(Sex.Male, "Fall 39", fall39);
				}
				
				return fall39;
			}
		}
		
		public static List<TestProfile> Fall40
		{
			get
			{
				if(fall40==null)
				{
					fall40 = new List<TestProfile>()
					{
						null,
						new TestProfile("±,0", "±,0", "-,-", "0,+"),
						new TestProfile("±,-", "±,0", "±,0", "0,+"),//2
						new TestProfile("+,-", "±,0", "-,0", "0,±"),
						new TestProfile("+,-", "±,+", "-,0", "0,±"),
						new TestProfile("±,-", "±,+", "±,0", "0,±"),//5
						
						new TestProfile("+,-", "±,0", "±,0", "0,±"),//6,8,10
						new TestProfile("+,-", "±,0", "±,-", "0,+"),
						new TestProfile("+,-", "±,0", "±,0", "0,±"),//6,8,10
						new TestProfile("+,-", "±,+", "±,0", "0,±"),//9
						new TestProfile("+,-", "±,0", "±,0", "0,±"),//6,8,10
					};
					BaseSzondiUnitTests.SetSexAndNameForProfiles(Sex.Male, "Fall 40", fall40);
				}
				
				return fall40;
			}
		}
		
		
		public static List<TestProfile> Fall42
		{
			get
			{
				if(fall42==null)
				{
					fall42 = new List<TestProfile>()
					{
						null,
						new TestProfile("+,+", "0,-!", "0,±", "-,+"),
						new TestProfile("+,+", "+,-!!", "±,+", "-,+"),
						new TestProfile("+,0", "±,-", "±,±", "-,+"),
						new TestProfile("+,+", "-,-", "-,±", "-,+"),
						new TestProfile("+,+", "±,-", "±,±", "-,0"),//5
						
						new TestProfile("+,+!", "-,-", "0,0", "-!,+"),//6
						new TestProfile("+,+", "+,-", "+,±", "-,-"),
						new TestProfile("+,+", "0,-!", "±,+", "-,0"),
						new TestProfile("+,+", "+,-!", "±,±", "-,0"),
						new TestProfile("+,+!", "0,-!", "±,+", "-,-"),
						
						new TestProfile("+,+!", "-,0", "+,±", "-!,-"),//11 p.503
					};
					BaseSzondiUnitTests.SetSexAndNameForProfiles(Sex.Male, "Fall 42", fall42);
				}
				
				return fall42;
			}
		}
		
		public static List<TestProfile> B3Tab49Mann
		{
			get
			{
				if(b3Tab49Mann==null)
				{
					b3Tab49Mann = new List<TestProfile>()
					{
						null,
						new TestProfile("+,-", "+,-", "0,±", "+,+"),
						new TestProfile("+,-!", "0,-", "0,±", "+!,+"),
						new TestProfile("+,-", "0,-", "-,±", "+,±"),
						new TestProfile("+,-", "0,-", "-,±", "+,+"),
						new TestProfile("0,±", "0,-", "-,±", "+,+"),//5
						
						new TestProfile("0,-!!", "0,±", "0,±", "+,+"),//6
						new TestProfile("±,-!!", "0,±", "0,±", "+,0"),
						new TestProfile("+,-", "0,±", "0,±", "+,+"),
						new TestProfile("-,-!!", "0,-", "0,±", "+!,+"),			
						new TestProfile("±,-!", "+,0", "0,±", "0,+!"),//10
						new TestProfile("±,±", "0,-", "0,±", "0,0"),
						new TestProfile("±,±", "0,0", "0,+!", "0,0"),
						new TestProfile("±,-!", "+,-", "0,+!", "0,0"),
						new TestProfile("+,±", "-,-", "0,+!", "0,+"),//14
					};
					BaseSzondiUnitTests.SetSexAndNameForProfiles(Sex.Male, 
					                                             "B3Tab49 Mann (not serie)", 
					                                             b3Tab49Mann);
				}
				
				return b3Tab49Mann;
			}
		}
		
		public static List<TestProfile> B3Tab49Frau
		{
			get
			{
				if(b3Tab49Frau==null)
				{
					b3Tab49Frau = new List<TestProfile>()
					{
						null,
						new TestProfile("-,+", "-,+", "±,0", "+,+"),
						new TestProfile("-,+", "-,0", "±,0", "0,0"),
						new TestProfile("±,+", "±,0", "±,0", "+,+"),
						new TestProfile("±,+", "+,±", "0,0", "-,0"),
						new TestProfile("±,+", "0,-", "0,0", "+,±"),//5
						
						new TestProfile("±,+", "-,+", "+!,0", "±,±"),
						new TestProfile("-!,-!", "-,+", "+,-", "+,±"),
						new TestProfile("-!,-!", "±,+", "+!,+", "0,0"),
						new TestProfile("±,±", "-,+", "0,+", "0,+"),
						new TestProfile("-,±", "±,+", "+!,0", "-,0"),//10
						
						new TestProfile("+,0", "+,±", "±,-!", "0,0"),
						new TestProfile("-,0", "-,+", "+,-", "+,+"),
						new TestProfile("-,-!", "±,-", "0,0", "+,+"),
						new TestProfile("±,-!", "0,±", "±,0", "+,0"),//14 
					};
					BaseSzondiUnitTests.SetSexAndNameForProfiles(Sex.Female, 
					                                             "B3Tab49 Frau (not serie)",
					                                             b3Tab49Frau);
				}
				
				return b3Tab49Frau;
			}
		}
	
		public static List<TestProfile> B3Tab50I
		{
			get
			{
				if(b3Tab50I==null)
				{
					b3Tab50I = new List<TestProfile>()
					{
						null,
						new TestProfile("±,-", "±,0", "±,0", "0,±"),
						new TestProfile("±,-", "±,+", "±,0", "0,±"),
					};
					BaseSzondiUnitTests.SetSexAndNameForProfiles(Sex.Male, "B3 Tab.50 I", b3Tab50I);
				}
				
				return b3Tab50I;
			}
		}
		
		public static List<TestProfile> B3Tab50II
		{
			get
			{
				if(b3Tab50II==null)
				{
					b3Tab50II = new List<TestProfile>()
					{
						null,
						new TestProfile("±,0", "+,±", "-,0", "+,0"),
						new TestProfile("±,0", "-,-", "±,0", "+,0"),
					};
					BaseSzondiUnitTests.SetSexAndNameForProfiles(Sex.Male, "B3 Tab.50 II", b3Tab50II);
				}
				
				return b3Tab50II;
			}
		}
		
		public static List<TestProfile> B3Tab50III
		{
			get
			{
				if(b3Tab50III==null)
				{
					b3Tab50III = new List<TestProfile>()
					{
						null,
						new TestProfile("±,-!", "+,+", "0,±", "0,-"),
						new TestProfile("±,-!", "0,+", "+,±", "+,0"),
					};
					BaseSzondiUnitTests.SetSexAndNameForProfiles(Sex.Male, "B3 Tab.50 III", b3Tab50III);
				}
				
				return b3Tab50III;
			}
		}
		
		public static List<TestProfile> B3Tab50IV
		{
			get
			{
				if(b3Tab50IV==null)
				{
					b3Tab50IV = new List<TestProfile>()
					{
						null,
						new TestProfile("±,+", "+,-", "±,+", "-,0"),
						new TestProfile("±,+", "±,-", "-,+", "±,0"),
					};
					BaseSzondiUnitTests.SetSexAndNameForProfiles(Sex.Male, "B3 Tab.50 IV", b3Tab50IV);
				}
				
				return b3Tab50IV;
			}
		}
		
		public static List<TestProfile> B2BiExi1
		{
			get
			{
				if(b2BiExi1==null)
				{
					b2BiExi1 = new List<TestProfile>()
					{
						null,
						new TestProfile("+,-", "+,0", "0,±", "+,+"),
					};
					BaseSzondiUnitTests.SetSexAndNameForProfiles(Sex.Male, "B2 Bi-Exi 1 p.430", b2BiExi1);
				}
				
				return b2BiExi1;
			}
		}
		
		public static List<TestProfile> B2BiExi2
		{
			get
			{
				if(b2BiExi2==null)
				{
					b2BiExi2 = new List<TestProfile>()
					{
						null,
						new TestProfile("+,-", "+,+", "+,-", "+,+"),
						new TestProfile("+,-", "0,0", "+,-", "+,+"),
					};
					BaseSzondiUnitTests.SetSexAndNameForProfiles(Sex.Male, "B2 Bi-Exi 2 p.430", b2BiExi2);
				}
				
				return b2BiExi2;
			}
		}
		
		public static List<TestProfile> B2Abb40
		{
			get
			{
				if(b2Abb40==null)
				{
					b2Abb40 = new List<TestProfile>()
					{
						null,
						new TestProfile("+,+", "0,-!", "-!,0", "+,+"),
					};
					BaseSzondiUnitTests.SetSexAndNameForProfiles(Sex.Female, "B2 Abb.40 p.421", b2Abb40);
				}
				
				return b2Abb40;
			}
		}
	}
}