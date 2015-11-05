using UnityEngine;

public interface Character {
	// kick is for now, we should replace with generics like neutralLight, neutralHeavy, and so on
	// the functions should describe the input that causes the move, not the move itself.
	// neutral light would be for neutral movement (no left, right or up input) and light attack button.
	void neutralHeavy(GameObject collider, GameObject hitter);
}