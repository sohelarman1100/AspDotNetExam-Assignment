using Autofac;
using AutoMapper;
using DataImporter.Functionality.BusinessObjects;
using DataImporter.Functionality.Services;
//using DataImporter.Membership.BusinessObjects;
//using DataImporter.Membership.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DataImporter.Areas.DataControlArea.Models
{
    public class EditGroupModel
    {
        [Required, Range(1, 500000)]
        public int? Id { get; set; }

        [Required, MaxLength(50, ErrorMessage = "name length should less than 50 character ")]
        public string GroupName { get; set; }

        public Guid UserId { get; set; }

        private readonly IGroupService _groupService;
        private readonly IMapper _mapper;
        public EditGroupModel()
        {
            _groupService = Startup.AutofacContainer.Resolve<IGroupService>();
            _mapper = Startup.AutofacContainer.Resolve<IMapper>();
        }

        public EditGroupModel(IGroupService groupService)
        {
            _groupService = groupService;
        }
        internal void EditGroup(int id)
        {
            var data = _groupService.EditGroup(id);
            _mapper.Map(data,this);
        }

        internal void UpdateGroup()
        {
            var group = _mapper.Map<GroupBO>(this);
            _groupService.UpdateGroup(group);
        }

        internal void DeleteGroup(int id)
        {
            _groupService.DeleteGroup(id);
        }
    }
}
