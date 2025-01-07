namespace IjaOrisha
{
    public class BattleMessage
    {
        public static void OnSocketMessage(SocMessage message)
        {
            string socResponse = message.action;

            switch (socResponse)
            {
                case "sessionJoined":
                    EventPub.Emit(PlayEvent.OnSessionJoined);
                    break;
                case "sessionStart":
                    EventPub.Emit(PlayEvent.OnSessionStart);
                    break;
                case "formationStart":
                    EventPub.Emit(PlayEvent.OnFormationStart);
                    break;
                case "formationEnd":
                    if (EventSub.InFormation)
                        EventPub.Emit(PlayEvent.OnFormationEnd);
                    break;
                case "battleData":
                    BattlePlayer.SetBattleData(message);
                    EventPub.Emit(PlayEvent.OnBattleData);
                    break;
                case "sessionEnd":
                    BattlePlayer.SetBattleData(message);
                    BattlePlayer.FindWinner();
                    EventPub.Emit(PlayEvent.OnSessionEnd);
                    break;
            }
        }
    }
}