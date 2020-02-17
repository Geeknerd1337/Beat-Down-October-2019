// Shader created with Shader Forge v1.38 
// Shader Forge (c) Freya Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.38;sub:START;pass:START;ps:flbk:,iptp:0,cusa:False,bamd:0,cgin:,lico:1,lgpr:1,limd:1,spmd:1,trmd:0,grmd:0,uamb:True,mssp:True,bkdf:False,hqlp:False,rprd:False,enco:False,rmgx:True,imps:True,rpth:0,vtps:0,hqsc:True,nrmq:1,nrsp:0,vomd:0,spxs:False,tesm:0,olmd:1,culm:0,bsrc:3,bdst:7,dpts:2,wrdp:False,dith:0,atcv:False,rfrpo:True,rfrpn:Refraction,coma:15,ufog:True,aust:True,igpj:True,qofs:0,qpre:3,rntp:2,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0.5,fgcg:0.5,fgcb:0.5,fgca:1,fgde:0.01,fgrn:0,fgrf:300,stcl:False,atwp:False,stva:128,stmr:255,stmw:255,stcp:6,stps:0,stfa:0,stfz:0,ofsf:0,ofsu:0,f2p0:False,fnsp:False,fnfb:False,fsmp:False;n:type:ShaderForge.SFN_Final,id:4013,x:32719,y:32712,varname:node_4013,prsc:2|diff-6743-RGB,emission-693-OUT,alpha-3559-OUT;n:type:ShaderForge.SFN_Color,id:1304,x:32620,y:32541,ptovrint:False,ptlb:Color,ptin:_Color,varname:node_1304,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,c1:1,c2:1,c3:1,c4:1;n:type:ShaderForge.SFN_TexCoord,id:6635,x:32063,y:33072,varname:node_6635,prsc:2,uv:0,uaff:False;n:type:ShaderForge.SFN_Lerp,id:6012,x:32272,y:33082,varname:node_6012,prsc:2|A-8671-OUT,B-3476-OUT,T-6635-V;n:type:ShaderForge.SFN_Vector1,id:8671,x:32083,y:33010,varname:node_8671,prsc:2,v1:1;n:type:ShaderForge.SFN_Clamp01,id:3559,x:32463,y:33068,varname:node_3559,prsc:2|IN-6012-OUT;n:type:ShaderForge.SFN_Slider,id:3476,x:31851,y:32929,ptovrint:False,ptlb:node_3476,ptin:_node_3476,varname:node_3476,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:0.7638891,max:1;n:type:ShaderForge.SFN_Color,id:4018,x:32197,y:32572,ptovrint:False,ptlb:portalColor,ptin:_portalColor,varname:node_4018,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,c1:0.4009434,c2:0.9478753,c3:1,c4:1;n:type:ShaderForge.SFN_Tex2d,id:6743,x:32032,y:32710,ptovrint:False,ptlb:node_6743,ptin:_node_6743,varname:node_6743,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:ee69c1f0e0c3ace49937de030c9c8682,ntxv:0,isnm:False|UVIN-3854-UVOUT;n:type:ShaderForge.SFN_Panner,id:3854,x:31841,y:32685,varname:node_3854,prsc:2,spu:0.2,spv:-2|UVIN-7377-UVOUT;n:type:ShaderForge.SFN_TexCoord,id:7377,x:31578,y:32657,varname:node_7377,prsc:2,uv:0,uaff:False;n:type:ShaderForge.SFN_Multiply,id:4738,x:32370,y:32945,varname:node_4738,prsc:2|A-4018-RGB,B-7412-OUT;n:type:ShaderForge.SFN_Vector1,id:7412,x:32219,y:32989,varname:node_7412,prsc:2,v1:0.8;n:type:ShaderForge.SFN_Multiply,id:6407,x:32429,y:32767,varname:node_6407,prsc:2|A-6743-RGB,B-6743-A,C-4283-RGB;n:type:ShaderForge.SFN_Add,id:693,x:32572,y:32846,varname:node_693,prsc:2|A-6407-OUT,B-4738-OUT;n:type:ShaderForge.SFN_Color,id:4283,x:32184,y:32809,ptovrint:False,ptlb:node_4283,ptin:_node_4283,varname:node_4283,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,c1:0.5,c2:0.5,c3:0.5,c4:1;proporder:1304-3476-4018-6743-4283;pass:END;sub:END;*/

Shader "Shader Forge/Portal" {
    Properties {
        _Color ("Color", Color) = (1,1,1,1)
        _node_3476 ("node_3476", Range(0, 1)) = 0.7638891
        _portalColor ("portalColor", Color) = (0.4009434,0.9478753,1,1)
        _node_6743 ("node_6743", 2D) = "white" {}
        _node_4283 ("node_4283", Color) = (0.5,0.5,0.5,1)
        [HideInInspector]_Cutoff ("Alpha cutoff", Range(0,1)) = 0.5
    }
    SubShader {
        Tags {
            "IgnoreProjector"="True"
            "Queue"="Transparent"
            "RenderType"="Transparent"
        }
        Pass {
            Name "FORWARD"
            Tags {
                "LightMode"="ForwardBase"
            }
            Blend SrcAlpha OneMinusSrcAlpha
            ZWrite Off
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_FORWARDBASE
            #include "UnityCG.cginc"
            #pragma multi_compile_fwdbase
            #pragma multi_compile_fog
            #pragma only_renderers d3d9 d3d11 glcore gles 
            #pragma target 3.0
            uniform float4 _LightColor0;
            uniform float _node_3476;
            uniform float4 _portalColor;
            uniform sampler2D _node_6743; uniform float4 _node_6743_ST;
            uniform float4 _node_4283;
            struct VertexInput {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
                float2 texcoord0 : TEXCOORD0;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float2 uv0 : TEXCOORD0;
                float4 posWorld : TEXCOORD1;
                float3 normalDir : TEXCOORD2;
                UNITY_FOG_COORDS(3)
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
                o.normalDir = UnityObjectToWorldNormal(v.normal);
                o.posWorld = mul(unity_ObjectToWorld, v.vertex);
                float3 lightColor = _LightColor0.rgb;
                o.pos = UnityObjectToClipPos( v.vertex );
                UNITY_TRANSFER_FOG(o,o.pos);
                return o;
            }
            float4 frag(VertexOutput i) : COLOR {
                i.normalDir = normalize(i.normalDir);
                float3 normalDirection = i.normalDir;
                float3 lightDirection = normalize(_WorldSpaceLightPos0.xyz);
                float3 lightColor = _LightColor0.rgb;
////// Lighting:
                float attenuation = 1;
                float3 attenColor = attenuation * _LightColor0.xyz;
/////// Diffuse:
                float NdotL = max(0.0,dot( normalDirection, lightDirection ));
                float3 directDiffuse = max( 0.0, NdotL) * attenColor;
                float3 indirectDiffuse = float3(0,0,0);
                indirectDiffuse += UNITY_LIGHTMODEL_AMBIENT.rgb; // Ambient Light
                float4 node_6746 = _Time;
                float2 node_3854 = (i.uv0+node_6746.g*float2(0.2,-2));
                float4 _node_6743_var = tex2D(_node_6743,TRANSFORM_TEX(node_3854, _node_6743));
                float3 diffuseColor = _node_6743_var.rgb;
                float3 diffuse = (directDiffuse + indirectDiffuse) * diffuseColor;
////// Emissive:
                float3 node_4738 = (_portalColor.rgb*0.8);
                float3 emissive = ((_node_6743_var.rgb*_node_6743_var.a*_node_4283.rgb)+node_4738);
/// Final Color:
                float3 finalColor = diffuse + emissive;
                float node_6012 = lerp(1.0,_node_3476,i.uv0.g);
                fixed4 finalRGBA = fixed4(finalColor,saturate(node_6012));
                UNITY_APPLY_FOG(i.fogCoord, finalRGBA);
                return finalRGBA;
            }
            ENDCG
        }
        Pass {
            Name "FORWARD_DELTA"
            Tags {
                "LightMode"="ForwardAdd"
            }
            Blend One One
            ZWrite Off
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_FORWARDADD
            #include "UnityCG.cginc"
            #include "AutoLight.cginc"
            #pragma multi_compile_fwdadd
            #pragma multi_compile_fog
            #pragma only_renderers d3d9 d3d11 glcore gles 
            #pragma target 3.0
            uniform float4 _LightColor0;
            uniform float _node_3476;
            uniform float4 _portalColor;
            uniform sampler2D _node_6743; uniform float4 _node_6743_ST;
            uniform float4 _node_4283;
            struct VertexInput {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
                float2 texcoord0 : TEXCOORD0;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float2 uv0 : TEXCOORD0;
                float4 posWorld : TEXCOORD1;
                float3 normalDir : TEXCOORD2;
                LIGHTING_COORDS(3,4)
                UNITY_FOG_COORDS(5)
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
                o.normalDir = UnityObjectToWorldNormal(v.normal);
                o.posWorld = mul(unity_ObjectToWorld, v.vertex);
                float3 lightColor = _LightColor0.rgb;
                o.pos = UnityObjectToClipPos( v.vertex );
                UNITY_TRANSFER_FOG(o,o.pos);
                TRANSFER_VERTEX_TO_FRAGMENT(o)
                return o;
            }
            float4 frag(VertexOutput i) : COLOR {
                i.normalDir = normalize(i.normalDir);
                float3 normalDirection = i.normalDir;
                float3 lightDirection = normalize(lerp(_WorldSpaceLightPos0.xyz, _WorldSpaceLightPos0.xyz - i.posWorld.xyz,_WorldSpaceLightPos0.w));
                float3 lightColor = _LightColor0.rgb;
////// Lighting:
                float attenuation = LIGHT_ATTENUATION(i);
                float3 attenColor = attenuation * _LightColor0.xyz;
/////// Diffuse:
                float NdotL = max(0.0,dot( normalDirection, lightDirection ));
                float3 directDiffuse = max( 0.0, NdotL) * attenColor;
                float4 node_5393 = _Time;
                float2 node_3854 = (i.uv0+node_5393.g*float2(0.2,-2));
                float4 _node_6743_var = tex2D(_node_6743,TRANSFORM_TEX(node_3854, _node_6743));
                float3 diffuseColor = _node_6743_var.rgb;
                float3 diffuse = directDiffuse * diffuseColor;
/// Final Color:
                float3 finalColor = diffuse;
                float node_6012 = lerp(1.0,_node_3476,i.uv0.g);
                fixed4 finalRGBA = fixed4(finalColor * saturate(node_6012),0);
                UNITY_APPLY_FOG(i.fogCoord, finalRGBA);
                return finalRGBA;
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
    CustomEditor "ShaderForgeMaterialInspector"
}
