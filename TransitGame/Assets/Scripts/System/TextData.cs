using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;

public static class TextData {

	public static List<ArrayList> getData(int data_num)
	{
		List<ArrayList> data = new List<ArrayList>();
		if (data_num == 0)
		{
			data.Add(EventData.addCompass());
			data.Add(EventData.addCharacterData(0));
			data.Add(EventData.addTextData("JM: Hey", 1));
			data.Add(EventData.addTextData("W: Yeah?", 0));
			data.Add(EventData.addTextData("JM: I was wondering how you're feeling", 1));
			data.Add(EventData.addTextData("W: About what?", 0));
			data.Add(EventData.addChoiceData("JM: About sailing.", 12, "It's okay", "I don't know", 1, 2, 1));
			data.Add(EventData.addTextData("JM: That's okay", 1));
			data.Add(EventData.addTextData("JM: 1", 0));
			data.Add(EventData.addTextData("JM: 2", 0));
		}
		else if (data_num == 1)
		{
			data.Add(EventData.addCompass());
			data.Add(EventData.addCharacterData(1));
			data.Add(EventData.addCharacterData(2));
			data.Add(EventData.addTextData("JohnMichael: I think we have around 2 more hours of rental time on this boat.", 1));
			data.Add(EventData.addTextData("JohnMichael: You're being awfully quiet today.", 1));
			data.Add(EventData.addTextData("Will: ...", 2));
			data.Add(EventData.addChoiceData("JM: How comfortable are you with sailing?", 12, "Anyone can sail", "I don't know how to sail", 6, 1, -1));
			data.Add(EventData.addTextData("Will: I've never sailed in my life.", 2));
			data.Add(EventData.addTextData("JohnMichael: You play computer games right?", 1));
			data.Add(EventData.addTextData("Will: Yeah.", 2));
			data.Add(EventData.addTextData("JohnMichael: Visualize a mouse. Imagine if you pressed the left click button you'd move in that direction.", 1));
			data.Add(EventData.addTextData("JohnMichael: Driving a boat is surprisingly simple..", 1));
			data.Add(EventData.addTextData("JohnMichael: Here's the keys and a compass. Try sticking to the compass because we might run out of gas-", 1));
			data.Add(EventData.addTextData("JohnMichael: but other than that just try to relax.", 1));
			data.Add(EventData.addTextData("Will: Isn't this dangerous?.", 2));
			data.Add(EventData.addTextData("JohnMichael: Not unless you want it to be.", 1));
		}
		else if (data_num == 2)
		{
			data.Add(EventData.addCharacterData(1));
			data.Add(EventData.addCharacterData(2));
			data.Add(EventData.addTextData("JohnMichael: We're 10 miles away from the coast. This is as far as we can be to Santa Barbara to spread ashes.", 1));
			data.Add(EventData.addTextData("JohnMichael: Are You ready?", 1));
			data.Add(EventData.addTextData("Will: ...", 2));
			data.Add(EventData.addChoiceData("JM: Or do you want to keep sailing?", 12, "I'm ready", "I want to sail", 1, 30, -1));
			data.Add(EventData.addTextData("Will: I feel like I never really gave him a proper good bye.", 2));
			data.Add(EventData.addTextData("JohnMichael: Your mom's getting the ashes from the back.", 1));
			data.Add(EventData.addTextData("JohnMichael: You can spend this time to think of what to say.", 1));
			data.Add(EventData.addTextData("Will: ...", 2));
			data.Add(EventData.addTextData("Will: ...", 2));
			data.Add(EventData.addTextData("Will: ...", 2));
			
			data.Add(EventData.addChoiceData("What do I want to say?", 5, "Something he would like", "Something I would like", "Something mom would like", 1, 1, 1, -3));
			
			data.Add(EventData.addTextData("JohnMichael: We're saying our good byes right now.", 1));
			data.Add(EventData.addChoiceData("JohnMichael: Do you have anything you'd like to say?", 5, "Yes", "No...", 1, 19, 19));
			
			data.Add(EventData.addTextData("Will: Dear John", 2));
			data.Add(EventData.addTextData("Will: by Cheryl Marita", 2));
			data.Add(EventData.addTextData("You left early that day to join", 2));
			data.Add(EventData.addTextData("your siblings at the white gate.", 2));
			data.Add(EventData.addTextData("The hum of the tractor motor", 2));
			data.Add(EventData.addTextData("sang your spirit into the next world.", 2));
			data.Add(EventData.addTextData("You left quickly, quietly-dialogue was never your", 2));
			data.Add(EventData.addTextData("forte.  Blue jays, woodpeckers", 2));
			data.Add(EventData.addTextData("joined in a chorus to raise your spirit.", 2));
			data.Add(EventData.addTextData("Buddy sat sentinel, ever  loyal.", 2));
			data.Add(EventData.addTextData("Trees looked down with tenderness.", 2));
			data.Add(EventData.addTextData("Birds sang songs you now understand.", 2));
			data.Add(EventData.addTextData("Sunlight sent rays to light your passage.", 2));
			data.Add(EventData.addTextData("Earth embraced you.", 2));
			data.Add(EventData.addTextData("Earth, nourish the saplings John planted.", 2));
			data.Add(EventData.addTextData("Lake, sustain the fish he loved to catch.", 2));
			data.Add(EventData.addTextData("Sky, bring rains to feed the earth he loved.", 2));
			data.Add(EventData.addTextData("Spirit, soothe us in our sorrow.", 2));
			data.Add(EventData.addTextData("Will: ...", 2));
			
			data.Add(EventData.addEndGame());
			data.Add(EventData.addTextData("Will: ...", 2));
			
		}
		return data;
	}
	
}
