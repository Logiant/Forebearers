public static class Constants {

	/*
	 * ACTION CONSTANTS
	 */
//	public static readonly uint numACTIONS = 6;
	public enum ACTION {
		NONE = 0,
		TALK,
		TRADE,
		BECKON,
		FOLLOW,
		ATTACK,
		BRIBE
	}

	public static readonly string[] ACTIONSTR = {"NONE", "TALK", "TRADE", "BECKON", "FOLLOW", "ATTACK", "BRIBE" };

}
