using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserContacts.Bll.Dtos;
using UserContacts.Bll.MappingProfiles;
using UserContacts.Core.Errors;
using UserContacts.Dal;
using UserContacts.Dal.Entities;

namespace UserContacts.Bll.Services
{
    public class ContactService : IContactService
    {
        private readonly ILogger<ContactService> logger;
        private readonly MainContext _mainContext;
        private readonly IMapper _mapper;
        public ContactService(MainContext mainContext, IMapper mapper)
        {
            _mainContext = mainContext;
            _mapper = mapper;
        }
        public async Task<long> AddContactAsync(ContactCreateDto contactCreateDto, long userId)
        {

            var contactEntity = _mapper.Map<Contact>(contactCreateDto);
            contactEntity.UserId = userId;
            _mainContext.Contacts.Add(contactEntity);
            await _mainContext.SaveChangesAsync();
            return contactEntity.ContactId;
        }

        public async Task DeleteContactAsync(long contactId, long userId)
        {
            var contact = await GetContactByIdAsync(contactId, userId);
            _mainContext.Remove(contact);
            await _mainContext.SaveChangesAsync();
        }

        public async  Task<List<ContactDto>> GetAllContactsAsync(long userId)
        {
            var contacts = await _mainContext.Contacts
            .Where(c => c.UserId == userId)
            .ToListAsync();

            return _mapper.Map<List<ContactDto>>(contacts);
        }

        public async Task<ContactDto> GetContactByIdAsync(long contactId, long userId)
        {
            var contactById = await _mainContext.Contacts
              .FirstOrDefaultAsync(c => c.ContactId == contactId && c.UserId == userId);

            if (contactById == null)
            {
                throw new EntityNotFoundException("contact by this userId or this contactId not found ");
            }

            return _mapper.Map<ContactDto>(contactById);
        }

        public async Task UpdateContactAsync(ContactDto contactDto, long userId)
        {
            var contact = _mapper.Map<Contact>(GetContactByIdAsync(contactDto.ContactId, userId));
            _mapper.Map(contactDto, contact);
            _mainContext.Contacts.Update(contact);
            await _mainContext.SaveChangesAsync();
        }
    }
}
