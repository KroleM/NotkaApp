﻿using NotkaAPI.Contracts;
using NotkaAPI.Data;

namespace NotkaAPI.Repository
{
	public class RepositoryWrapper : IRepositoryWrapper
	{
		private NotkaDatabaseContext _context;
		private IUserRepository? _user;
		private IRoleRepository? _role;
		private INoteRepository? _note;
		private ITagRepository? _tag;
		private IListRepository? _list;

		public IUserRepository User
		{
			get
			{
				if (_user == null)
				{
					_user = new UserRepository(_context);
				}

				return _user;
			}
		}
		public IRoleRepository Role
		{
			get
			{
				if (_role == null)
				{
					_role = new RoleRepository(_context);
				}

				return _role;
			}
		}
		public INoteRepository Note
		{
			get
			{
				if (_note == null)
				{
					_note = new NoteRepository(_context);
				}

				return _note;
			}
		}
		public ITagRepository Tag
		{
			get
			{
				if (_tag == null)
				{
					_tag = new TagRepository(_context);
				}

				return _tag;
			}
		}
		public IListRepository List
		{
			get
			{
				if (_list == null)
				{
					_list = new ListRepository(_context);
				}

				return _list;
			}
		}

		public RepositoryWrapper(NotkaDatabaseContext context)
		{
			_context = context;
		}

		public async Task SaveAsync()
		{
			await _context.SaveChangesAsync();
		}
	}
}
