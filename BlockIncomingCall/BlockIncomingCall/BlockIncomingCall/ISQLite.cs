using System;
using System.Collections.Generic;
using System.Text;

namespace BlockIncomingCall
{
    public interface ISQLite
    {
        SQLite.SQLiteConnection GetConnection();
    }
}
