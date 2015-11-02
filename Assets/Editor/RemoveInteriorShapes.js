    @MenuItem("GameObject/Remove Interior Shapes", true)
    static function Validate () {
        var go = Selection.activeObject as GameObject;
        if (!go) {
            return false;
        }
        var pCollider = go.GetComponent(PolygonCollider2D);
        if (!pCollider) {
            return false;
        }
        if (pCollider.pathCount < 2) {
            return false;
        }
        return true;
    }
     
    @MenuItem ("GameObject/Remove Interior Shapes")
    static function RemoveInteriorShapes () {
        var pCollider = Selection.activeObject.GetComponent(PolygonCollider2D);
        Undo.RecordObject (pCollider, "Remove Interior Shapes");
        var exteriorShape = 0;
        var leftmostPoint = Mathf.Infinity;
        for (var i = 0; i < pCollider.pathCount; i++) {
            var path = pCollider.GetPath (i);
            for (var j = 0; j < path.Length; j++) {
                if (path[j].x < leftmostPoint) {
                    exteriorShape = i;
                    leftmostPoint = path[j].x;
                }
            }
        }
       
        path = pCollider.GetPath (exteriorShape);
        pCollider.pathCount = 1;
        pCollider.SetPath (0, path);
    }
