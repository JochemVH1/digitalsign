using System;

namespace digitalsign.common.Enumeration
{
    [Flags]
    public enum UserRole
    {
        None = 0 << 0,
        User = 1 << 1,
        Admin = 2 << 2,
        System = 4 << 3
    }
}
