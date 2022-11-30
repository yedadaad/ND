Shader "Custom/Mask"
{
    SubShader
    {
        Tags{"Queue" = "Geometry-10"}

Lighting off

//相当于小于或者等于本身深度值时，该物体渲染

ZTest LEqual

//打开深度写入

ZWrite On

//通道遮罩，为0时不写入任何颜色通道，除了深度缓存

ColorMask 0

Pass{}
    }
}
