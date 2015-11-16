using UnityEngine;

public interface Character {
	// the functions should describe the input that causes the move, not the move itself.
	// neutral light would be for neutral movement (no left, right or up input) and light attack button.
	void neutralLight(GameObject collider, GameObject hitter);
	void neutralHeavy(GameObject collider, GameObject hitter);
}