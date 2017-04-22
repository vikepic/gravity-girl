--- Simple Gravity ---

Set-up:

To use simple gravity, simply apply the Gravity component (or Gravity2D for 2D scenes) to the GameObject you 
wish to be the centre of your force. This can be applied to any GameObject in your scene, and to as many as 
you want or even apply multiple components to the same object. Then tweak the values to get the desired effect.

*Reverse Force - This reverses the direction of the force to switch between attract and repel.

*Strength - A multiplier on the strength of the force.

*Strength Exponent - Exponent on the decay of the strength of the force over distance. A Higher exponent makes
the force get weaker over a shorter distance.

*Scale Strength On Mass - When checked, the force is multiplied by the rigidbody mass of the GameObject the
Gravity component is on. This can be used for space-gravity simulation.

*Range - The maximum range at which the Gravity component will affect objects. Decrease this range to affect 
less objects to improve efficiency. (Only really necessary if uses a LOT of objects)

*Target Tag - The GameObject tag this component will have an effect on. Make sure GameObjects you want to apply
force to are tagged with the same name you enter here.




Notes: 

-The force is calculated by the formula x/distance^y, where x is strength and y is the strength exponent.

-Target Tag can also be used to optimise. For example, there may no point applying a small force to massive objects.

-Try using mutliple components on the same Object with different exponents. Using a low attract exponent and a high
 repel exponent will cause affected objects to settle a certain distance away. (As shown in the demos)

-The difference between the 3D and 2D versions is the rigidbodys and colliders they use. If you are using a 3D
 scene from a 2D perspective, use the 3D component.


If you have any questions at all, please e-mail me at mwbeardsell@gmail.com

Thank you very much for purchasing Simple Forces!





