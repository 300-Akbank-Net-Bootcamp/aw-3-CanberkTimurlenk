﻿using MediatR;
using Vb.Base.Response;
using Vb.Schema;

namespace Vb.Business.Features.Contacts.Queries.GetById;
public record GetContactByIdQuery(int Id) : IRequest<ApiResponse<ContactResponse>>;