namespace CTRE
{
    public static class StatusCodes
    {
        public const int OK = 0;

        //CAN Error Codes
        public const int CAN_MSG_STALE = 1;
        public const int CAN_TX_FULL = -1;
        public const int CAN_INVALID_PARAM = -2;
        public const int CAN_MSG_NOT_FOUND = -3;
        public const int CAN_NO_MORE_TX_JOBS = -4;
        public const int CAN_NO_SESSIONS_AVAIL = -5;
        public const int CAN_OVERFLOW = -6;

        public const int GENERAL_ERROR = -100;

        public const int SIG_NOT_UPDATED = -200;

        //Gadgeteer Port Error Codes
        //These include errors between ports and modules
        public const int GEN_PORT_ERROR = -300;
        public const int PORT_MODULE_TYPE_MISMATCH = -301;

        //Gadgeteer Module Error Codes
        //These apply only to the module units themselves
        public const int GEN_MODULE_ERROR = -400;
        public const int MODULE_NOT_INIT_SET_ERROR = -401;
        public const int MODULE_NOT_INIT_GET_ERROR = -402;
    }
}