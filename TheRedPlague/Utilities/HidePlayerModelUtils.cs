using UnityEngine;

namespace TheRedPlague.Utilities;

public static class HidePlayerModelUtils
{
	private static GameObject _playerRoot;
	
	private static void EnsurePlayerRootExists()
	{
		if (_playerRoot == null)
		{
			_playerRoot = Player.main.transform.Find("body/player_view").gameObject;
		}
	}

	public static void SetPlayerModelActive(bool active)
	{
		EnsurePlayerRootExists();
		if (_playerRoot != null)
			_playerRoot.SetActive(active);
		else
			Plugin.Logger.LogWarning("Player root object not found!");
	}
}