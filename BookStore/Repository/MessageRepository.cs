using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookStore.Models;
using Microsoft.Extensions.Options;

namespace BookStore.Repository
{
    public class MessageRepository : IMessageRepository
    {
        private readonly IOptionsMonitor<NewBookAlertConfig> _newBookAlertConfig;

        public MessageRepository(IOptionsMonitor<NewBookAlertConfig> newBookAlertConfig)
        {
            _newBookAlertConfig = newBookAlertConfig;
        }
        public string GetName()
        {
            return _newBookAlertConfig.CurrentValue.BookName;
        }
    }
}
