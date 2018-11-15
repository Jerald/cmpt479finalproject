## Code model

Class's are represented as nodes on our graph. Edges represent a data dependency between nodes.

Our model has nodes stored in a map, keyed with some ID (possibly class name). The node class contains two maps, `incoming` and `outgoing` which are keyed with an incoming or outgoing node respectively, and whose value is an `edge` object holding the information about the depdendency.