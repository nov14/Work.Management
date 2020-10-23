using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Work.Api.Entities;

namespace Work.Api.Models
{
    /// <summary>
    /// 令牌类，用于存储客户端的一些基本信息
    /// </summary>
    public class TokenModel
    {
        /// <summary>
        /// 用户id
        /// </summary>
        public int Uid { get; set; }
        /// <summary>
        /// 用户名
        /// </summary>
        public string Uname { get; set; }
        /// <summary>
        /// 角色
        /// </summary>
        public Role Role { get; set; }

        //与User的一对一关系
        public int UserId { get; set; }
        public User User { get; set; }

    }
}
