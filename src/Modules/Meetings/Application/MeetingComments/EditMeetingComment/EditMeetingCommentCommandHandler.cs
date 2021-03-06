﻿using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using CompanyName.MyMeetings.BuildingBlocks.Application;
using CompanyName.MyMeetings.Modules.Meetings.Application.Configuration.Commands;
using CompanyName.MyMeetings.Modules.Meetings.Domain.Comments;
using CompanyName.MyMeetings.Modules.Meetings.Domain.MeetingComments;
using CompanyName.MyMeetings.Modules.Meetings.Domain.Members;
using MediatR;

namespace CompanyName.MyMeetings.Modules.Meetings.Application.MeetingComments.EditMeetingComment
{
    public class EditMeetingCommentCommandHandler : ICommandHandler<EditMeetingCommentCommand, Unit>
    {
        private readonly IMeetingCommentRepository _meetingCommentRepository;
        private readonly IMemberContext _memberContext;

        public EditMeetingCommentCommandHandler(IMeetingCommentRepository meetingCommentRepository, IMemberContext memberContext)
        {
            _meetingCommentRepository = meetingCommentRepository;
            _memberContext = memberContext;
        }

        public async Task<Unit> Handle(EditMeetingCommentCommand command, CancellationToken cancellationToken)
        {
            var meetingComment = await _meetingCommentRepository.GetByIdAsync(new MeetingCommentId(command.MeetingCommentId));
            if (meetingComment == null)
            {
                throw new InvalidCommandException(new List<string> { "Meeting comment for editing must exist." });
            }

            meetingComment.Edit(_memberContext.MemberId, command.EditedComment);

            return Unit.Value;
        }
    }
}