select t.Name, t.GenreId, t.Composer, g.Name from Track as t
	inner join Genre as g
	on t.GenreId = g.GenreId
	where g.Name = 'pop'
	order by t.Name;