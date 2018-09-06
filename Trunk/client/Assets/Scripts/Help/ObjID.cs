using System;

namespace SysUtils
{
    
    // 对象ID
    public struct ObjID
    {
        public uint m_nIdent;
        public uint m_nSerial ;

        public static ObjID zero = new ObjID(0, 0);

        public ObjID(uint ident, uint serial)
        {
            m_nIdent = ident;
            m_nSerial = serial;
        }

        public ObjID Clone()
        {
            return new ObjID(m_nIdent, m_nSerial);
        }

        public bool IsNull()
        {
            if (m_nIdent != 0)
            {
                return false;
            }

            if (m_nSerial != 0)
            {
                return false;
            }
            return true;
        }

        public bool EqualTo(ObjID other)
        {
            return (m_nIdent == other.m_nIdent)
                && (m_nSerial == other.m_nSerial);
        }
        
        public override string ToString()
        {
            return string.Format("{0}-{1}", m_nIdent, m_nSerial);
        }
        
        public static ObjID FromString(string val)
        {
            int index = val.IndexOf("-");

            if (index == -1)
            {
                return ObjID.zero;

            }

            uint ident = Convert.ToUInt32(val.Substring(0, index));
            uint serial = Convert.ToUInt32(val.Substring(index + 1,
                val.Length - index - 1));

            return new ObjID(ident, serial);
        }
        
        public override bool Equals(System.Object obj)
        {
            try
            {
                return EqualTo((ObjID)obj);
            }
            catch (Exception)
            {
            }

            return false;
        }

        public override int GetHashCode()
        {
            return (int)(m_nIdent + m_nSerial );
        }

        public static bool operator ==(ObjID p1, ObjID p2)
        {
            return (p1.m_nIdent == p2.m_nIdent) && (p1.m_nSerial == p2.m_nSerial);
        }

        public static bool operator !=(ObjID p1, ObjID p2)
        {
            return (p1.m_nIdent != p2.m_nIdent) || (p1.m_nSerial != p2.m_nSerial);
        }
    }
}