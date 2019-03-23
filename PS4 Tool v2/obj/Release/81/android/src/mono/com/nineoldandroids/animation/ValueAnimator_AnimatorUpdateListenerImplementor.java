package mono.com.nineoldandroids.animation;


public class ValueAnimator_AnimatorUpdateListenerImplementor
	extends java.lang.Object
	implements
		mono.android.IGCUserPeer,
		com.nineoldandroids.animation.ValueAnimator.AnimatorUpdateListener
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"n_onAnimationUpdate:(Lcom/nineoldandroids/animation/ValueAnimator;)V:GetOnAnimationUpdate_Lcom_nineoldandroids_animation_ValueAnimator_Handler:Xamarin.NineOldAndroids.Animations.ValueAnimator/IAnimatorUpdateListenerInvoker, NineOldAndroids\n" +
			"";
		mono.android.Runtime.register ("Xamarin.NineOldAndroids.Animations.ValueAnimator+IAnimatorUpdateListenerImplementor, NineOldAndroids", ValueAnimator_AnimatorUpdateListenerImplementor.class, __md_methods);
	}


	public ValueAnimator_AnimatorUpdateListenerImplementor ()
	{
		super ();
		if (getClass () == ValueAnimator_AnimatorUpdateListenerImplementor.class)
			mono.android.TypeManager.Activate ("Xamarin.NineOldAndroids.Animations.ValueAnimator+IAnimatorUpdateListenerImplementor, NineOldAndroids", "", this, new java.lang.Object[] {  });
	}


	public void onAnimationUpdate (com.nineoldandroids.animation.ValueAnimator p0)
	{
		n_onAnimationUpdate (p0);
	}

	private native void n_onAnimationUpdate (com.nineoldandroids.animation.ValueAnimator p0);

	private java.util.ArrayList refList;
	public void monodroidAddReference (java.lang.Object obj)
	{
		if (refList == null)
			refList = new java.util.ArrayList ();
		refList.add (obj);
	}

	public void monodroidClearReferences ()
	{
		if (refList != null)
			refList.clear ();
	}
}
