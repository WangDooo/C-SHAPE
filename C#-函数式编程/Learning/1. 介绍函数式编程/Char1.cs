1.1.1 函数作为第一类值

Func<int, int> triple = x => x * 3;
var range = Enumerable.Range(1,3);
var triples = range.Select(triple);

