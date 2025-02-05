using UnityEngine;

namespace PlayFreely.HotfixRuntime
{
    /// <summary>
    /// 实体数据
    /// </summary>
    public class EntityData:IReference
    {
        /// <summary>
        /// id
        /// </summary>
        [SerializeField]
        private int m_ID = 0;

        [SerializeField]
        private int m_TypeId = 0;

        [SerializeField]
        private Vector3 m_Position = Vector3.zero;

        [SerializeField]
        private Quaternion m_Rotation = Quaternion.identity;

        public EntityData(int entityId , int typeId)
        {
            m_ID = entityId;
            m_TypeId = typeId;
        }

        /// <summary>
        /// 实体ID
        /// </summary>
        public int ID
        {
            get
            {
                return m_ID;
            }
        }

        /// <summary>
        /// 实体类型编号
        /// </summary>
        public int TypeID
        {
            get
            {
                return m_TypeId;
            }
        }

        /// <summary>
        /// 实体位置
        /// </summary>
        public Vector3 Position
        {
            get
            {
                return m_Position;
            }
            set
            {
                m_Position = value;
            }
        }

        /// <summary>
        /// 实体朝向
        /// </summary>
        public Quaternion Rotation
        {
            get
            {
                return m_Rotation;
            }
            set
            {
                m_Rotation = value;
            }
        }

        /// <summary>
        /// 填充实体数据
        /// </summary>
        /// <param name="id">实体ID</param>
        /// <param name="typeId">实体类型编号</param>
        protected void Fill(int id , int typeId)
        {
            m_ID = id;
            m_TypeId = typeId;
        }

        public virtual void Clear( )
        {
            m_ID = 0;
            m_TypeId = 0;
            m_Position = default;
            m_Rotation = default;
        }
    }
}
