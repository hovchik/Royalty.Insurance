using MediatR;
using Microsoft.EntityFrameworkCore;
using Application.Interfaces;
using Royalty.Insurance.Proxy.Response;
using Royalty.Insurance.Settings;
using System.Common.Exceptions;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Domain;

namespace Royalty.Insurance.BusinessLayer.FlagmanWebHook
{
    internal class CreateCallRecordCommandHandler : IRequestHandler<CreateCallRecordCommand, BaseResponse<bool>>
    {
        private readonly IUserPhoneCallHistoryMapperService _mapper;
        private readonly IApplicationDbContext _context;

        public CreateCallRecordCommandHandler(IUserPhoneCallHistoryMapperService mapper, IApplicationDbContext context)
        {
            _mapper = mapper;
            _context = context;
        }

        public async Task<BaseResponse<bool>> Handle(CreateCallRecordCommand request, CancellationToken cancellationToken)
        {
            UserPhoneCallHistory entity = new UserPhoneCallHistory();
            string callId = request.CallId.ToLower().Trim();
            using (var transaction = await _context.BeginTransactionAsync(cancellationToken))
            {
                try
                {
                    var record = await _context.UserPhoneCallHistories.FirstOrDefaultAsync(x => x.CallId.ToLower().Equals(callId), cancellationToken);
                    if (record == null)
                    {
                        _mapper.UpdateEntity(entity, request);
                        await _context.UserPhoneCallHistories.AddAsync(entity, cancellationToken);
                    }
                    else
                    {
                        _mapper.ModifyEntity(record, request);
                        _context.UserPhoneCallHistories.Update(record);
                    }

                    if (await _context.SaveChangesAsync(cancellationToken) != 1)
                    {
                        throw new RestApiResponseException((int)HttpStatusCode.InternalServerError,
                            ResourceCommonMessage.SaveFailed);
                    }

                    await transaction.CommitAsync(cancellationToken);
                }
                catch
                {
                    await transaction.RollbackAsync(cancellationToken);

                    return new BaseResponse<bool>(false);
                }
            }

            return new BaseResponse<bool>(true);
        }
    }
}