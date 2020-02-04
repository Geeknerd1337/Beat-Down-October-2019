// Shader created with Shader Forge v1.38 
// Shader Forge (c) Freya Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.38;sub:START;pass:START;ps:flbk:,iptp:0,cusa:False,bamd:0,cgin:,lico:1,lgpr:1,limd:1,spmd:1,trmd:0,grmd:0,uamb:True,mssp:True,bkdf:False,hqlp:False,rprd:False,enco:False,rmgx:True,imps:True,rpth:0,vtps:0,hqsc:True,nrmq:1,nrsp:0,vomd:0,spxs:False,tesm:0,olmd:1,culm:0,bsrc:3,bdst:7,dpts:2,wrdp:False,dith:0,atcv:False,rfrpo:True,rfrpn:Refraction,coma:15,ufog:True,aust:True,igpj:True,qofs:0,qpre:3,rntp:2,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0.5,fgcg:0.5,fgcb:0.5,fgca:1,fgde:0.01,fgrn:0,fgrf:300,stcl:False,atwp:False,stva:128,stmr:255,stmw:255,stcp:6,stps:0,stfa:0,stfz:0,ofsf:0,ofsu:0,f2p0:False,fnsp:False,fnfb:False,fsmp:False;n:type:ShaderForge.SFN_Final,id:4013,x:32719,y:32712,varname:node_4013,prsc:2|diff-2291-RGB,emission-4679-OUT,alpha-3578-OUT;n:type:ShaderForge.SFN_Color,id:2291,x:31924,y:32881,ptovrint:False,ptlb:forceFieldColor,ptin:_forceFieldColor,varname:node_2291,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,c1:0.0235849,c2:1,c3:0.9127226,c4:1;n:type:ShaderForge.SFN_Tex2d,id:5862,x:31746,y:33101,ptovrint:False,ptlb:scanLines,ptin:_scanLines,varname:node_5862,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:e15cefda0414c8545b290e8654984ca2,ntxv:0,isnm:False|UVIN-1380-UVOUT;n:type:ShaderForge.SFN_Multiply,id:4679,x:32214,y:32958,varname:node_4679,prsc:2|A-2291-RGB,B-5862-RGB,C-436-OUT,D-9613-OUT,E-4074-OUT;n:type:ShaderForge.SFN_Panner,id:1380,x:31484,y:33111,varname:node_1380,prsc:2,spu:1,spv:1|UVIN-711-UVOUT;n:type:ShaderForge.SFN_TexCoord,id:711,x:31756,y:33274,varname:node_711,prsc:2,uv:0,uaff:False;n:type:ShaderForge.SFN_ValueProperty,id:3578,x:32199,y:32717,ptovrint:False,ptlb:alpha,ptin:_alpha,varname:node_3578,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:0.2;n:type:ShaderForge.SFN_Tex2d,id:1851,x:32031,y:33241,ptovrint:False,ptlb:noise,ptin:_noise,varname:node_1851,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:28c7aad1372ff114b90d330f8a2dd938,ntxv:0,isnm:False;n:type:ShaderForge.SFN_RemapRange,id:436,x:32175,y:33218,varname:node_436,prsc:2,frmn:0,frmx:1,tomn:0.5,tomx:1|IN-1851-RGB;n:type:ShaderForge.SFN_ValueProperty,id:9613,x:32059,y:33118,ptovrint:False,ptlb:glow_amt,ptin:_glow_amt,varname:node_9613,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:4;n:type:ShaderForge.SFN_Fresnel,id:7805,x:32378,y:33218,varname:node_7805,prsc:2|EXP-1050-OUT;n:type:ShaderForge.SFN_Slider,id:1050,x:32221,y:33376,ptovrint:False,ptlb:fresnel,ptin:_fresnel,varname:node_1050,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:0.6961247,max:3;n:type:ShaderForge.SFN_RemapRange,id:4074,x:32556,y:33150,varname:node_4074,prsc:2,frmn:0,frmx:1,tomn:1,tomx:0|IN-7805-OUT;proporder:2291-5862-3578-1851-9613-1050;pass:END;sub:END;*/

Shader "Shader Forge/Forcefield" {
    Properties {
        _forceFieldColor ("forceFieldColor", Color) = (0.0235849,1,0.9127226,1)
        _scanLines ("scanLines", 2D) = "white" {}
        _alpha ("alpha", Float ) = 0.2
        _noise ("noise", 2D) = "white" {}
        _glow_amt ("glow_amt", Float ) = 4
        _fresnel ("fresnel", Range(0, 3)) = 0.6961247
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
            uniform float4 _forceFieldColor;
            uniform sampler2D _scanLines; uniform float4 _scanLines_ST;
            uniform float _alpha;
            uniform sampler2D _noise; uniform float4 _noise_ST;
            uniform float _glow_amt;
            uniform float _fresnel;
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
                float3 viewDirection = normalize(_WorldSpaceCameraPos.xyz - i.posWorld.xyz);
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
                float3 diffuseColor = _forceFieldColor.rgb;
                float3 diffuse = (directDiffuse + indirectDiffuse) * diffuseColor;
////// Emissive:
                float4 node_363 = _Time;
                float2 node_1380 = (i.uv0+node_363.g*float2(1,1));
                float4 _scanLines_var = tex2D(_scanLines,TRANSFORM_TEX(node_1380, _scanLines));
                float4 _noise_var = tex2D(_noise,TRANSFORM_TEX(i.uv0, _noise));
                float node_4074 = (pow(1.0-max(0,dot(normalDirection, viewDirection)),_fresnel)*-1.0+1.0);
                float3 node_4679 = (_forceFieldColor.rgb*_scanLines_var.rgb*(_noise_var.rgb*0.5+0.5)*_glow_amt*node_4074);
                float3 emissive = node_4679;
/// Final Color:
                float3 finalColor = diffuse + emissive;
                fixed4 finalRGBA = fixed4(finalColor,_alpha);
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
            uniform float4 _forceFieldColor;
            uniform sampler2D _scanLines; uniform float4 _scanLines_ST;
            uniform float _alpha;
            uniform sampler2D _noise; uniform float4 _noise_ST;
            uniform float _glow_amt;
            uniform float _fresnel;
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
                float3 viewDirection = normalize(_WorldSpaceCameraPos.xyz - i.posWorld.xyz);
                float3 normalDirection = i.normalDir;
                float3 lightDirection = normalize(lerp(_WorldSpaceLightPos0.xyz, _WorldSpaceLightPos0.xyz - i.posWorld.xyz,_WorldSpaceLightPos0.w));
                float3 lightColor = _LightColor0.rgb;
////// Lighting:
                float attenuation = LIGHT_ATTENUATION(i);
                float3 attenColor = attenuation * _LightColor0.xyz;
/////// Diffuse:
                float NdotL = max(0.0,dot( normalDirection, lightDirection ));
                float3 directDiffuse = max( 0.0, NdotL) * attenColor;
                float3 diffuseColor = _forceFieldColor.rgb;
                float3 diffuse = directDiffuse * diffuseColor;
/// Final Color:
                float3 finalColor = diffuse;
                fixed4 finalRGBA = fixed4(finalColor * _alpha,0);
                UNITY_APPLY_FOG(i.fogCoord, finalRGBA);
                return finalRGBA;
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
    CustomEditor "ShaderForgeMaterialInspector"
}
