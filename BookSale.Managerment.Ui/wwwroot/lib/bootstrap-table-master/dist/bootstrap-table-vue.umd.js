(function(Q,re){typeof exports=="object"&&typeof module<"u"?module.exports=re():typeof define=="function"&&define.amd?define(re):(Q=typeof globalThis<"u"?globalThis:Q||self,Q.BootstrapTable=re())})(this,function(){"use strict";var Q={};/**
* @vue/shared v3.5.13
* (c) 2018-present Yuxi (Evan) You and Vue contributors
* @license MIT
**//*! #__NO_SIDE_EFFECTS__ */function re(e){const t=Object.create(null);for(const n of e.split(","))t[n]=1;return n=>n in t}const $=Q.NODE_ENV!=="production"?Object.freeze({}):{},en=Q.NODE_ENV!=="production"?Object.freeze([]):[],se=()=>{},tn=e=>e.charCodeAt(0)===111&&e.charCodeAt(1)===110&&(e.charCodeAt(2)>122||e.charCodeAt(2)<97),T=Object.assign,nn=Object.prototype.hasOwnProperty,w=(e,t)=>nn.call(e,t),b=Array.isArray,Z=e=>ge(e)==="[object Map]",rn=e=>ge(e)==="[object Set]",S=e=>typeof e=="function",M=e=>typeof e=="string",ie=e=>typeof e=="symbol",y=e=>e!==null&&typeof e=="object",sn=e=>(y(e)||S(e))&&S(e.then)&&S(e.catch),on=Object.prototype.toString,ge=e=>on.call(e),dt=e=>ge(e).slice(8,-1),cn=e=>ge(e)==="[object Object]",He=e=>M(e)&&e!=="NaN"&&e[0]!=="-"&&""+parseInt(e,10)===e,ln=(e=>{const t=Object.create(null);return n=>t[n]||(t[n]=e(n))})(e=>e.charAt(0).toUpperCase()+e.slice(1)),B=(e,t)=>!Object.is(e,t),an=(e,t,n,s=!1)=>{Object.defineProperty(e,t,{configurable:!0,enumerable:!1,writable:s,value:n})};let ht;const _e=()=>ht||(ht=typeof globalThis<"u"?globalThis:typeof self<"u"?self:typeof window<"u"?window:typeof global<"u"?global:{});function We(e){if(b(e)){const t={};for(let n=0;n<e.length;n++){const s=e[n],r=M(s)?dn(s):We(s);if(r)for(const i in r)t[i]=r[i]}return t}else if(M(e)||y(e))return e}const un=/;(?![^(]*\))/g,fn=/:([^]+)/,pn=/\/\*[^]*?\*\//g;function dn(e){const t={};return e.replace(pn,"").split(un).forEach(n=>{if(n){const s=n.split(fn);s.length>1&&(t[s[0].trim()]=s[1].trim())}}),t}function Ke(e){let t="";if(M(e))t=e;else if(b(e))for(let n=0;n<e.length;n++){const s=Ke(e[n]);s&&(t+=s+" ")}else if(y(e))for(const n in e)e[n]&&(t+=n+" ");return t.trim()}var E={};function W(e,...t){console.warn(`[Vue warn] ${e}`,...t)}let m;const Le=new WeakSet;class hn{constructor(t){this.fn=t,this.deps=void 0,this.depsTail=void 0,this.flags=5,this.next=void 0,this.cleanup=void 0,this.scheduler=void 0}pause(){this.flags|=64}resume(){this.flags&64&&(this.flags&=-65,Le.has(this)&&(Le.delete(this),this.trigger()))}notify(){this.flags&2&&!(this.flags&32)||this.flags&8||gn(this)}run(){if(!(this.flags&1))return this.fn();this.flags|=2,Et(this),_t(this);const t=m,n=P;m=this,P=!0;try{return this.fn()}finally{E.NODE_ENV!=="production"&&m!==this&&W("Active effect was not restored correctly - this is likely a Vue internal bug."),mt(this),m=t,P=n,this.flags&=-3}}stop(){if(this.flags&1){for(let t=this.deps;t;t=t.nextDep)Je(t);this.deps=this.depsTail=void 0,Et(this),this.onStop&&this.onStop(),this.flags&=-2}}trigger(){this.flags&64?Le.add(this):this.scheduler?this.scheduler():this.runIfDirty()}runIfDirty(){Be(this)&&this.run()}get dirty(){return Be(this)}}let gt=0,oe,ce;function gn(e,t=!1){if(e.flags|=8,t){e.next=ce,ce=e;return}e.next=oe,oe=e}function ze(){gt++}function Ue(){if(--gt>0)return;if(ce){let t=ce;for(ce=void 0;t;){const n=t.next;t.next=void 0,t.flags&=-9,t=n}}let e;for(;oe;){let t=oe;for(oe=void 0;t;){const n=t.next;if(t.next=void 0,t.flags&=-9,t.flags&1)try{t.trigger()}catch(s){e||(e=s)}t=n}}if(e)throw e}function _t(e){for(let t=e.deps;t;t=t.nextDep)t.version=-1,t.prevActiveLink=t.dep.activeLink,t.dep.activeLink=t}function mt(e){let t,n=e.depsTail,s=n;for(;s;){const r=s.prevDep;s.version===-1?(s===n&&(n=r),Je(s),mn(s)):t=s,s.dep.activeLink=s.prevActiveLink,s.prevActiveLink=void 0,s=r}e.deps=t,e.depsTail=n}function Be(e){for(let t=e.deps;t;t=t.nextDep)if(t.dep.version!==t.version||t.dep.computed&&(_n(t.dep.computed)||t.dep.version!==t.version))return!0;return!!e._dirty}function _n(e){if(e.flags&4&&!(e.flags&16)||(e.flags&=-17,e.globalVersion===Ee))return;e.globalVersion=Ee;const t=e.dep;if(e.flags|=2,t.version>0&&!e.isSSR&&e.deps&&!Be(e)){e.flags&=-3;return}const n=m,s=P;m=e,P=!0;try{_t(e);const r=e.fn(e._value);(t.version===0||B(r,e._value))&&(e._value=r,t.version++)}catch(r){throw t.version++,r}finally{m=n,P=s,mt(e),e.flags&=-3}}function Je(e,t=!1){const{dep:n,prevSub:s,nextSub:r}=e;if(s&&(s.nextSub=r,e.prevSub=void 0),r&&(r.prevSub=s,e.nextSub=void 0),E.NODE_ENV!=="production"&&n.subsHead===e&&(n.subsHead=r),n.subs===e&&(n.subs=s,!s&&n.computed)){n.computed.flags&=-5;for(let i=n.computed.deps;i;i=i.nextDep)Je(i,!0)}!t&&!--n.sc&&n.map&&n.map.delete(n.key)}function mn(e){const{prevDep:t,nextDep:n}=e;t&&(t.nextDep=n,e.prevDep=void 0),n&&(n.prevDep=t,e.nextDep=void 0)}let P=!0;const bt=[];function me(){bt.push(P),P=!1}function be(){const e=bt.pop();P=e===void 0?!0:e}function Et(e){const{cleanup:t}=e;if(e.cleanup=void 0,t){const n=m;m=void 0;try{t()}finally{m=n}}}let Ee=0;class bn{constructor(t,n){this.sub=t,this.dep=n,this.version=n.version,this.nextDep=this.prevDep=this.nextSub=this.prevSub=this.prevActiveLink=void 0}}class En{constructor(t){this.computed=t,this.version=0,this.activeLink=void 0,this.subs=void 0,this.map=void 0,this.key=void 0,this.sc=0,E.NODE_ENV!=="production"&&(this.subsHead=void 0)}track(t){if(!m||!P||m===this.computed)return;let n=this.activeLink;if(n===void 0||n.sub!==m)n=this.activeLink=new bn(m,this),m.deps?(n.prevDep=m.depsTail,m.depsTail.nextDep=n,m.depsTail=n):m.deps=m.depsTail=n,wt(n);else if(n.version===-1&&(n.version=this.version,n.nextDep)){const s=n.nextDep;s.prevDep=n.prevDep,n.prevDep&&(n.prevDep.nextDep=s),n.prevDep=m.depsTail,n.nextDep=void 0,m.depsTail.nextDep=n,m.depsTail=n,m.deps===n&&(m.deps=s)}return E.NODE_ENV!=="production"&&m.onTrack&&m.onTrack(T({effect:m},t)),n}trigger(t){this.version++,Ee++,this.notify(t)}notify(t){ze();try{if(E.NODE_ENV!=="production")for(let n=this.subsHead;n;n=n.nextSub)n.sub.onTrigger&&!(n.sub.flags&8)&&n.sub.onTrigger(T({effect:n.sub},t));for(let n=this.subs;n;n=n.prevSub)n.sub.notify()&&n.sub.dep.notify()}finally{Ue()}}}function wt(e){if(e.dep.sc++,e.sub.flags&4){const t=e.dep.computed;if(t&&!e.dep.subs){t.flags|=20;for(let s=t.deps;s;s=s.nextDep)wt(s)}const n=e.dep.subs;n!==e&&(e.prevSub=n,n&&(n.nextSub=e)),E.NODE_ENV!=="production"&&e.dep.subsHead===void 0&&(e.dep.subsHead=e),e.dep.subs=e}}const qe=new WeakMap,J=Symbol(E.NODE_ENV!=="production"?"Object iterate":""),Ye=Symbol(E.NODE_ENV!=="production"?"Map keys iterate":""),le=Symbol(E.NODE_ENV!=="production"?"Array iterate":"");function x(e,t,n){if(P&&m){let s=qe.get(e);s||qe.set(e,s=new Map);let r=s.get(n);r||(s.set(n,r=new En),r.map=s,r.key=n),E.NODE_ENV!=="production"?r.track({target:e,type:t,key:n}):r.track()}}function K(e,t,n,s,r,i){const o=qe.get(e);if(!o){Ee++;return}const c=a=>{a&&(E.NODE_ENV!=="production"?a.trigger({target:e,type:t,key:n,newValue:s,oldValue:r,oldTarget:i}):a.trigger())};if(ze(),t==="clear")o.forEach(c);else{const a=b(e),f=a&&He(n);if(a&&n==="length"){const d=Number(s);o.forEach((l,u)=>{(u==="length"||u===le||!ie(u)&&u>=d)&&c(l)})}else switch((n!==void 0||o.has(void 0))&&c(o.get(n)),f&&c(o.get(le)),t){case"add":a?f&&c(o.get("length")):(c(o.get(J)),Z(e)&&c(o.get(Ye)));break;case"delete":a||(c(o.get(J)),Z(e)&&c(o.get(Ye)));break;case"set":Z(e)&&c(o.get(J));break}}Ue()}function X(e){const t=h(e);return t===e?t:(x(t,"iterate",le),V(e)?t:t.map(R))}function Ge(e){return x(e=h(e),"iterate",le),e}const wn={__proto__:null,[Symbol.iterator](){return Qe(this,Symbol.iterator,R)},concat(...e){return X(this).concat(...e.map(t=>b(t)?X(t):t))},entries(){return Qe(this,"entries",e=>(e[1]=R(e[1]),e))},every(e,t){return j(this,"every",e,t,void 0,arguments)},filter(e,t){return j(this,"filter",e,t,n=>n.map(R),arguments)},find(e,t){return j(this,"find",e,t,R,arguments)},findIndex(e,t){return j(this,"findIndex",e,t,void 0,arguments)},findLast(e,t){return j(this,"findLast",e,t,R,arguments)},findLastIndex(e,t){return j(this,"findLastIndex",e,t,void 0,arguments)},forEach(e,t){return j(this,"forEach",e,t,void 0,arguments)},includes(...e){return Ze(this,"includes",e)},indexOf(...e){return Ze(this,"indexOf",e)},join(e){return X(this).join(e)},lastIndexOf(...e){return Ze(this,"lastIndexOf",e)},map(e,t){return j(this,"map",e,t,void 0,arguments)},pop(){return ae(this,"pop")},push(...e){return ae(this,"push",e)},reduce(e,...t){return Nt(this,"reduce",e,t)},reduceRight(e,...t){return Nt(this,"reduceRight",e,t)},shift(){return ae(this,"shift")},some(e,t){return j(this,"some",e,t,void 0,arguments)},splice(...e){return ae(this,"splice",e)},toReversed(){return X(this).toReversed()},toSorted(e){return X(this).toSorted(e)},toSpliced(...e){return X(this).toSpliced(...e)},unshift(...e){return ae(this,"unshift",e)},values(){return Qe(this,"values",R)}};function Qe(e,t,n){const s=Ge(e),r=s[t]();return s!==e&&!V(e)&&(r._next=r.next,r.next=()=>{const i=r._next();return i.value&&(i.value=n(i.value)),i}),r}const Nn=Array.prototype;function j(e,t,n,s,r,i){const o=Ge(e),c=o!==e&&!V(e),a=o[t];if(a!==Nn[t]){const l=a.apply(e,i);return c?R(l):l}let f=n;o!==e&&(c?f=function(l,u){return n.call(this,R(l),u,e)}:n.length>2&&(f=function(l,u){return n.call(this,l,u,e)}));const d=a.call(o,f,s);return c&&r?r(d):d}function Nt(e,t,n,s){const r=Ge(e);let i=n;return r!==e&&(V(e)?n.length>3&&(i=function(o,c,a){return n.call(this,o,c,a,e)}):i=function(o,c,a){return n.call(this,o,R(c),a,e)}),r[t](i,...s)}function Ze(e,t,n){const s=h(e);x(s,"iterate",le);const r=s[t](...n);return(r===-1||r===!1)&&Oe(n[0])?(n[0]=h(n[0]),s[t](...n)):r}function ae(e,t,n=[]){me(),ze();const s=h(e)[t].apply(e,n);return Ue(),be(),s}const Sn=re("__proto__,__v_isRef,__isVue"),St=new Set(Object.getOwnPropertyNames(Symbol).filter(e=>e!=="arguments"&&e!=="caller").map(e=>Symbol[e]).filter(ie));function On(e){ie(e)||(e=String(e));const t=h(this);return x(t,"has",e),t.hasOwnProperty(e)}class Ot{constructor(t=!1,n=!1){this._isReadonly=t,this._isShallow=n}get(t,n,s){if(n==="__v_skip")return t.__v_skip;const r=this._isReadonly,i=this._isShallow;if(n==="__v_isReactive")return!r;if(n==="__v_isReadonly")return r;if(n==="__v_isShallow")return i;if(n==="__v_raw")return s===(r?i?Tt:Dt:i?$n:yt).get(t)||Object.getPrototypeOf(t)===Object.getPrototypeOf(s)?t:void 0;const o=b(t);if(!r){let a;if(o&&(a=wn[n]))return a;if(n==="hasOwnProperty")return On}const c=Reflect.get(t,n,D(t)?t:s);return(ie(n)?St.has(n):Sn(n))||(r||x(t,"get",n),i)?c:D(c)?o&&He(n)?c:c.value:y(c)?r?Rt(c):Vt(c):c}}class xn extends Ot{constructor(t=!1){super(!1,t)}set(t,n,s,r){let i=t[n];if(!this._isShallow){const a=L(i);if(!V(s)&&!L(s)&&(i=h(i),s=h(s)),!b(t)&&D(i)&&!D(s))return a?!1:(i.value=s,!0)}const o=b(t)&&He(n)?Number(n)<t.length:w(t,n),c=Reflect.set(t,n,s,D(t)?t:r);return t===h(r)&&(o?B(s,i)&&K(t,"set",n,s,i):K(t,"add",n,s)),c}deleteProperty(t,n){const s=w(t,n),r=t[n],i=Reflect.deleteProperty(t,n);return i&&s&&K(t,"delete",n,void 0,r),i}has(t,n){const s=Reflect.has(t,n);return(!ie(n)||!St.has(n))&&x(t,"has",n),s}ownKeys(t){return x(t,"iterate",b(t)?"length":J),Reflect.ownKeys(t)}}class xt extends Ot{constructor(t=!1){super(!0,t)}set(t,n){return E.NODE_ENV!=="production"&&W(`Set operation on key "${String(n)}" failed: target is readonly.`,t),!0}deleteProperty(t,n){return E.NODE_ENV!=="production"&&W(`Delete operation on key "${String(n)}" failed: target is readonly.`,t),!0}}const vn=new xn,yn=new xt,Dn=new xt(!0),Xe=e=>e,we=e=>Reflect.getPrototypeOf(e);function Tn(e,t,n){return function(...s){const r=this.__v_raw,i=h(r),o=Z(i),c=e==="entries"||e===Symbol.iterator&&o,a=e==="keys"&&o,f=r[e](...s),d=n?Xe:t?tt:R;return!t&&x(i,"iterate",a?Ye:J),{next(){const{value:l,done:u}=f.next();return u?{value:l,done:u}:{value:c?[d(l[0]),d(l[1])]:d(l),done:u}},[Symbol.iterator](){return this}}}}function Ne(e){return function(...t){if(E.NODE_ENV!=="production"){const n=t[0]?`on key "${t[0]}" `:"";W(`${ln(e)} operation ${n}failed: target is readonly.`,h(this))}return e==="delete"?!1:e==="clear"?void 0:this}}function Vn(e,t){const n={get(r){const i=this.__v_raw,o=h(i),c=h(r);e||(B(r,c)&&x(o,"get",r),x(o,"get",c));const{has:a}=we(o),f=t?Xe:e?tt:R;if(a.call(o,r))return f(i.get(r));if(a.call(o,c))return f(i.get(c));i!==o&&i.get(r)},get size(){const r=this.__v_raw;return!e&&x(h(r),"iterate",J),Reflect.get(r,"size",r)},has(r){const i=this.__v_raw,o=h(i),c=h(r);return e||(B(r,c)&&x(o,"has",r),x(o,"has",c)),r===c?i.has(r):i.has(r)||i.has(c)},forEach(r,i){const o=this,c=o.__v_raw,a=h(c),f=t?Xe:e?tt:R;return!e&&x(a,"iterate",J),c.forEach((d,l)=>r.call(i,f(d),f(l),o))}};return T(n,e?{add:Ne("add"),set:Ne("set"),delete:Ne("delete"),clear:Ne("clear")}:{add(r){!t&&!V(r)&&!L(r)&&(r=h(r));const i=h(this);return we(i).has.call(i,r)||(i.add(r),K(i,"add",r,r)),this},set(r,i){!t&&!V(i)&&!L(i)&&(i=h(i));const o=h(this),{has:c,get:a}=we(o);let f=c.call(o,r);f?E.NODE_ENV!=="production"&&vt(o,c,r):(r=h(r),f=c.call(o,r));const d=a.call(o,r);return o.set(r,i),f?B(i,d)&&K(o,"set",r,i,d):K(o,"add",r,i),this},delete(r){const i=h(this),{has:o,get:c}=we(i);let a=o.call(i,r);a?E.NODE_ENV!=="production"&&vt(i,o,r):(r=h(r),a=o.call(i,r));const f=c?c.call(i,r):void 0,d=i.delete(r);return a&&K(i,"delete",r,void 0,f),d},clear(){const r=h(this),i=r.size!==0,o=E.NODE_ENV!=="production"?Z(r)?new Map(r):new Set(r):void 0,c=r.clear();return i&&K(r,"clear",void 0,void 0,o),c}}),["keys","values","entries",Symbol.iterator].forEach(r=>{n[r]=Tn(r,e,t)}),n}function ke(e,t){const n=Vn(e,t);return(s,r,i)=>r==="__v_isReactive"?!e:r==="__v_isReadonly"?e:r==="__v_raw"?s:Reflect.get(w(n,r)&&r in s?n:s,r,i)}const Rn={get:ke(!1,!1)},Cn={get:ke(!0,!1)},In={get:ke(!0,!0)};function vt(e,t,n){const s=h(n);if(s!==n&&t.call(e,s)){const r=dt(e);W(`Reactive ${r} contains both the raw and reactive versions of the same object${r==="Map"?" as keys":""}, which can lead to inconsistencies. Avoid differentiating between the raw and reactive versions of an object and only use the reactive version if possible.`)}}const yt=new WeakMap,$n=new WeakMap,Dt=new WeakMap,Tt=new WeakMap;function Pn(e){switch(e){case"Object":case"Array":return 1;case"Map":case"Set":case"WeakMap":case"WeakSet":return 2;default:return 0}}function An(e){return e.__v_skip||!Object.isExtensible(e)?0:Pn(dt(e))}function Vt(e){return L(e)?e:et(e,!1,vn,Rn,yt)}function Rt(e){return et(e,!0,yn,Cn,Dt)}function Se(e){return et(e,!0,Dn,In,Tt)}function et(e,t,n,s,r){if(!y(e))return E.NODE_ENV!=="production"&&W(`value cannot be made ${t?"readonly":"reactive"}: ${String(e)}`),e;if(e.__v_raw&&!(t&&e.__v_isReactive))return e;const i=r.get(e);if(i)return i;const o=An(e);if(o===0)return e;const c=new Proxy(e,o===2?s:n);return r.set(e,c),c}function k(e){return L(e)?k(e.__v_raw):!!(e&&e.__v_isReactive)}function L(e){return!!(e&&e.__v_isReadonly)}function V(e){return!!(e&&e.__v_isShallow)}function Oe(e){return e?!!e.__v_raw:!1}function h(e){const t=e&&e.__v_raw;return t?h(t):e}function Mn(e){return!w(e,"__v_skip")&&Object.isExtensible(e)&&an(e,"__v_skip",!0),e}const R=e=>y(e)?Vt(e):e,tt=e=>y(e)?Rt(e):e;function D(e){return e?e.__v_isRef===!0:!1}function Fn(e){return D(e)?e.value:e}const jn={get:(e,t,n)=>t==="__v_raw"?e:Fn(Reflect.get(e,t,n)),set:(e,t,n,s)=>{const r=e[t];return D(r)&&!D(n)?(r.value=n,!0):Reflect.set(e,t,n,s)}};function Hn(e){return k(e)?e:new Proxy(e,jn)}const xe={},ve=new WeakMap;let q;function Wn(e,t=!1,n=q){if(n){let s=ve.get(n);s||ve.set(n,s=[]),s.push(e)}else E.NODE_ENV!=="production"&&!t&&W("onWatcherCleanup() was called when there was no active watcher to associate with.")}function Kn(e,t,n=$){const{immediate:s,deep:r,once:i,scheduler:o,augmentJob:c,call:a}=n,f=_=>{(n.onWarn||W)("Invalid watch source: ",_,"A watch source can only be a getter/effect function, a ref, a reactive object, or an array of these types.")},d=_=>r?_:V(_)||r===!1||r===0?z(_,1):z(_);let l,u,p,N,I=!1,Fe=!1;if(D(e)?(u=()=>e.value,I=V(e)):k(e)?(u=()=>d(e),I=!0):b(e)?(Fe=!0,I=e.some(_=>k(_)||V(_)),u=()=>e.map(_=>{if(D(_))return _.value;if(k(_))return d(_);if(S(_))return a?a(_,2):_();E.NODE_ENV!=="production"&&f(_)})):S(e)?t?u=a?()=>a(e,2):e:u=()=>{if(p){me();try{p()}finally{be()}}const _=q;q=l;try{return a?a(e,3,[N]):e(N)}finally{q=_}}:(u=se,E.NODE_ENV!=="production"&&f(e)),t&&r){const _=u,F=r===!0?1/0:r;u=()=>z(_(),F)}const ne=()=>{l.stop()};if(i&&t){const _=t;t=(...F)=>{_(...F),ne()}}let G=Fe?new Array(e.length).fill(xe):xe;const he=_=>{if(!(!(l.flags&1)||!l.dirty&&!_))if(t){const F=l.run();if(r||I||(Fe?F.some((pt,je)=>B(pt,G[je])):B(F,G))){p&&p();const pt=q;q=l;try{const je=[F,G===xe?void 0:Fe&&G[0]===xe?[]:G,N];a?a(t,3,je):t(...je),G=F}finally{q=pt}}}else l.run()};return c&&c(he),l=new hn(u),l.scheduler=o?()=>o(he,!1):he,N=_=>Wn(_,!1,l),p=l.onStop=()=>{const _=ve.get(l);if(_){if(a)a(_,4);else for(const F of _)F();ve.delete(l)}},E.NODE_ENV!=="production"&&(l.onTrack=n.onTrack,l.onTrigger=n.onTrigger),t?s?he(!0):G=l.run():o?o(he.bind(null,!0),!0):l.run(),ne.pause=l.pause.bind(l),ne.resume=l.resume.bind(l),ne.stop=ne,ne}function z(e,t=1/0,n){if(t<=0||!y(e)||e.__v_skip||(n=n||new Set,n.has(e)))return e;if(n.add(e),t--,D(e))z(e.value,t,n);else if(b(e))for(let s=0;s<e.length;s++)z(e[s],t,n);else if(rn(e)||Z(e))e.forEach(s=>{z(s,t,n)});else if(cn(e)){for(const s in e)z(e[s],t,n);for(const s of Object.getOwnPropertySymbols(e))Object.prototype.propertyIsEnumerable.call(e,s)&&z(e[s],t,n)}return e}var g={};const Y=[];function Ln(e){Y.push(e)}function zn(){Y.pop()}let nt=!1;function O(e,...t){if(nt)return;nt=!0,me();const n=Y.length?Y[Y.length-1].component:null,s=n&&n.appContext.config.warnHandler,r=Un();if(s)ye(s,n,11,[e+t.map(i=>{var o,c;return(c=(o=i.toString)==null?void 0:o.call(i))!=null?c:JSON.stringify(i)}).join(""),n&&n.proxy,r.map(({vnode:i})=>`at <${Xt(n,i.type)}>`).join(`
`),r]);else{const i=[`[Vue warn]: ${e}`,...t];r.length&&i.push(`
`,...Bn(r)),console.warn(...i)}be(),nt=!1}function Un(){let e=Y[Y.length-1];if(!e)return[];const t=[];for(;e;){const n=t[0];n&&n.vnode===e?n.recurseCount++:t.push({vnode:e,recurseCount:0});const s=e.component&&e.component.parent;e=s&&s.vnode}return t}function Bn(e){const t=[];return e.forEach((n,s)=>{t.push(...s===0?[]:[`
`],...Jn(n))}),t}function Jn({vnode:e,recurseCount:t}){const n=t>0?`... (${t} recursive calls)`:"",s=e.component?e.component.parent==null:!1,r=` at <${Xt(e.component,e.type,s)}`,i=">"+n;return e.props?[r,...qn(e.props),i]:[r+i]}function qn(e){const t=[],n=Object.keys(e);return n.slice(0,3).forEach(s=>{t.push(...Ct(s,e[s]))}),n.length>3&&t.push(" ..."),t}function Ct(e,t,n){return M(t)?(t=JSON.stringify(t),n?t:[`${e}=${t}`]):typeof t=="number"||typeof t=="boolean"||t==null?n?t:[`${e}=${t}`]:D(t)?(t=Ct(e,h(t.value),!0),n?t:[`${e}=Ref<`,t,">"]):S(t)?[`${e}=fn${t.name?`<${t.name}>`:""}`]:(t=h(t),n?t:[`${e}=`,t])}const It={sp:"serverPrefetch hook",bc:"beforeCreate hook",c:"created hook",bm:"beforeMount hook",m:"mounted hook",bu:"beforeUpdate hook",u:"updated",bum:"beforeUnmount hook",um:"unmounted hook",a:"activated hook",da:"deactivated hook",ec:"errorCaptured hook",rtc:"renderTracked hook",rtg:"renderTriggered hook",0:"setup function",1:"render function",2:"watcher getter",3:"watcher callback",4:"watcher cleanup function",5:"native event handler",6:"component event handler",7:"vnode hook",8:"directive hook",9:"transition hook",10:"app errorHandler",11:"app warnHandler",12:"ref function",13:"async component loader",14:"scheduler flush",15:"component update",16:"app unmount cleanup function"};function ye(e,t,n,s){try{return s?e(...s):e()}catch(r){rt(r,t,n)}}function $t(e,t,n,s){if(S(e)){const r=ye(e,t,n,s);return r&&sn(r)&&r.catch(i=>{rt(i,t,n)}),r}if(b(e)){const r=[];for(let i=0;i<e.length;i++)r.push($t(e[i],t,n,s));return r}else g.NODE_ENV!=="production"&&O(`Invalid value type passed to callWithAsyncErrorHandling(): ${typeof e}`)}function rt(e,t,n,s=!0){const r=t?t.vnode:null,{errorHandler:i,throwUnhandledErrorInProduction:o}=t&&t.appContext.config||$;if(t){let c=t.parent;const a=t.proxy,f=g.NODE_ENV!=="production"?It[n]:`https://vuejs.org/error-reference/#runtime-${n}`;for(;c;){const d=c.ec;if(d){for(let l=0;l<d.length;l++)if(d[l](e,a,f)===!1)return}c=c.parent}if(i){me(),ye(i,null,10,[e,a,f]),be();return}}Yn(e,n,r,s,o)}function Yn(e,t,n,s=!0,r=!1){if(g.NODE_ENV!=="production"){const i=It[t];if(n&&Ln(n),O(`Unhandled error${i?` during execution of ${i}`:""}`),n&&zn(),s)throw e;console.error(e)}else{if(r)throw e;console.error(e)}}const C=[];let H=-1;const ee=[];let U=null,te=0;const Pt=Promise.resolve();let De=null;const Gn=100;function Qn(e){const t=De||Pt;return e?t.then(this?e.bind(this):e):t}function Zn(e){let t=H+1,n=C.length;for(;t<n;){const s=t+n>>>1,r=C[s],i=ue(r);i<e||i===e&&r.flags&2?t=s+1:n=s}return t}function st(e){if(!(e.flags&1)){const t=ue(e),n=C[C.length-1];!n||!(e.flags&2)&&t>=ue(n)?C.push(e):C.splice(Zn(t),0,e),e.flags|=1,At()}}function At(){De||(De=Pt.then(Ft))}function Mt(e){b(e)?ee.push(...e):U&&e.id===-1?U.splice(te+1,0,e):e.flags&1||(ee.push(e),e.flags|=1),At()}function Xn(e){if(ee.length){const t=[...new Set(ee)].sort((n,s)=>ue(n)-ue(s));if(ee.length=0,U){U.push(...t);return}for(U=t,g.NODE_ENV!=="production"&&(e=e||new Map),te=0;te<U.length;te++){const n=U[te];g.NODE_ENV!=="production"&&jt(e,n)||(n.flags&4&&(n.flags&=-2),n.flags&8||n(),n.flags&=-2)}U=null,te=0}}const ue=e=>e.id==null?e.flags&2?-1:1/0:e.id;function Ft(e){g.NODE_ENV!=="production"&&(e=e||new Map);const t=g.NODE_ENV!=="production"?n=>jt(e,n):se;try{for(H=0;H<C.length;H++){const n=C[H];if(n&&!(n.flags&8)){if(g.NODE_ENV!=="production"&&t(n))continue;n.flags&4&&(n.flags&=-2),ye(n,n.i,n.i?15:14),n.flags&4||(n.flags&=-2)}}}finally{for(;H<C.length;H++){const n=C[H];n&&(n.flags&=-2)}H=-1,C.length=0,Xn(e),De=null,(C.length||ee.length)&&Ft(e)}}function jt(e,t){const n=e.get(t)||0;if(n>Gn){const s=t.i,r=s&&Zt(s.type);return rt(`Maximum recursive updates exceeded${r?` in component <${r}>`:""}. This means you have a reactive effect that is mutating its own dependencies and thus recursively triggering itself. Possible sources include component template, render function, updated hook or watcher source function.`,null,10),!0}return e.set(t,n+1),!1}const it=new Map;g.NODE_ENV!=="production"&&(_e().__VUE_HMR_RUNTIME__={createRecord:ot(kn),rerender:ot(er),reload:ot(tr)});const Te=new Map;function kn(e,t){return Te.has(e)?!1:(Te.set(e,{initialDef:Ve(t),instances:new Set}),!0)}function Ve(e){return kt(e)?e.__vccOpts:e}function er(e,t){const n=Te.get(e);n&&(n.initialDef.render=t,[...n.instances].forEach(s=>{t&&(s.render=t,Ve(s.type).render=t),s.renderCache=[],s.update()}))}function tr(e,t){const n=Te.get(e);if(!n)return;t=Ve(t),Ht(n.initialDef,t);const s=[...n.instances];for(let r=0;r<s.length;r++){const i=s[r],o=Ve(i.type);let c=it.get(o);c||(o!==n.initialDef&&Ht(o,t),it.set(o,c=new Set)),c.add(i),i.appContext.propsCache.delete(i.type),i.appContext.emitsCache.delete(i.type),i.appContext.optionsCache.delete(i.type),i.ceReload?(c.add(i),i.ceReload(t.styles),c.delete(i)):i.parent?st(()=>{i.parent.update(),c.delete(i)}):i.appContext.reload?i.appContext.reload():typeof window<"u"?window.location.reload():console.warn("[HMR] Root or manually mounted instance modified. Full reload required."),i.root.ce&&i!==i.root&&i.root.ce._removeChildStyle(o)}Mt(()=>{it.clear()})}function Ht(e,t){T(e,t);for(const n in e)n!=="__file"&&!(n in t)&&delete e[n]}function ot(e){return(t,n)=>{try{return e(t,n)}catch(s){console.error(s),console.warn("[HMR] Something went wrong during Vue component hot-reload. Full reload required.")}}}let fe=null,nr=null;const rr=e=>e.__isTeleport;function Wt(e,t){e.shapeFlag&6&&e.component?(e.transition=t,Wt(e.component.subTree,t)):e.shapeFlag&128?(e.ssContent.transition=t.clone(e.ssContent),e.ssFallback.transition=t.clone(e.ssFallback)):e.transition=t}_e().requestIdleCallback,_e().cancelIdleCallback;const sr=Symbol.for("v-ndc"),ct=e=>e?Pr(e)?Ar(e):ct(e.parent):null,pe=T(Object.create(null),{$:e=>e,$el:e=>e.vnode.el,$data:e=>e.data,$props:e=>g.NODE_ENV!=="production"?Se(e.props):e.props,$attrs:e=>g.NODE_ENV!=="production"?Se(e.attrs):e.attrs,$slots:e=>g.NODE_ENV!=="production"?Se(e.slots):e.slots,$refs:e=>g.NODE_ENV!=="production"?Se(e.refs):e.refs,$parent:e=>ct(e.parent),$root:e=>ct(e.root),$host:e=>e.ce,$emit:e=>e.emit,$options:e=>or(e),$forceUpdate:e=>e.f||(e.f=()=>{st(e.update)}),$nextTick:e=>e.n||(e.n=Qn.bind(e.proxy)),$watch:e=>mr.bind(e)}),lt=(e,t)=>e!==$&&!e.__isScriptSetup&&w(e,t),ir={get({_:e},t){if(t==="__v_skip")return!0;const{ctx:n,setupState:s,data:r,props:i,accessCache:o,type:c,appContext:a}=e;if(g.NODE_ENV!=="production"&&t==="__isVue")return!0;let f;if(t[0]!=="$"){const p=o[t];if(p!==void 0)switch(p){case 1:return s[t];case 2:return r[t];case 4:return n[t];case 3:return i[t]}else{if(lt(s,t))return o[t]=1,s[t];if(r!==$&&w(r,t))return o[t]=2,r[t];if((f=e.propsOptions[0])&&w(f,t))return o[t]=3,i[t];if(n!==$&&w(n,t))return o[t]=4,n[t];o[t]=0}}const d=pe[t];let l,u;if(d)return t==="$attrs"?x(e.attrs,"get",""):g.NODE_ENV!=="production"&&t==="$slots"&&x(e,"get",t),d(e);if((l=c.__cssModules)&&(l=l[t]))return l;if(n!==$&&w(n,t))return o[t]=4,n[t];if(u=a.config.globalProperties,w(u,t))return u[t]},set({_:e},t,n){const{data:s,setupState:r,ctx:i}=e;return lt(r,t)?(r[t]=n,!0):g.NODE_ENV!=="production"&&r.__isScriptSetup&&w(r,t)?(O(`Cannot mutate <script setup> binding "${t}" from Options API.`),!1):s!==$&&w(s,t)?(s[t]=n,!0):w(e.props,t)?(g.NODE_ENV!=="production"&&O(`Attempting to mutate prop "${t}". Props are readonly.`),!1):t[0]==="$"&&t.slice(1)in e?(g.NODE_ENV!=="production"&&O(`Attempting to mutate public property "${t}". Properties starting with $ are reserved and readonly.`),!1):(g.NODE_ENV!=="production"&&t in e.appContext.config.globalProperties?Object.defineProperty(i,t,{enumerable:!0,configurable:!0,value:n}):i[t]=n,!0)},has({_:{data:e,setupState:t,accessCache:n,ctx:s,appContext:r,propsOptions:i}},o){let c;return!!n[o]||e!==$&&w(e,o)||lt(t,o)||(c=i[0])&&w(c,o)||w(s,o)||w(pe,o)||w(r.config.globalProperties,o)},defineProperty(e,t,n){return n.get!=null?e._.accessCache[t]=0:w(n,"value")&&this.set(e,t,n.value,null),Reflect.defineProperty(e,t,n)}};g.NODE_ENV!=="production"&&(ir.ownKeys=e=>(O("Avoid app logic that relies on enumerating keys on a component instance. The keys will be empty in production mode to avoid performance overhead."),Reflect.ownKeys(e)));function Kt(e){return b(e)?e.reduce((t,n)=>(t[n]=null,t),{}):e}function or(e){const t=e.type,{mixins:n,extends:s}=t,{mixins:r,optionsCache:i,config:{optionMergeStrategies:o}}=e.appContext,c=i.get(t);let a;return c?a=c:!r.length&&!n&&!s?a=t:(a={},r.length&&r.forEach(f=>Re(a,f,o,!0)),Re(a,t,o)),y(t)&&i.set(t,a),a}function Re(e,t,n,s=!1){const{mixins:r,extends:i}=t;i&&Re(e,i,n,!0),r&&r.forEach(o=>Re(e,o,n,!0));for(const o in t)if(s&&o==="expose")g.NODE_ENV!=="production"&&O('"expose" option is ignored when declared in mixins or extends. It should only be declared in the base component itself.');else{const c=cr[o]||n&&n[o];e[o]=c?c(e[o],t[o]):t[o]}return e}const cr={data:Lt,props:Ut,emits:Ut,methods:de,computed:de,beforeCreate:v,created:v,beforeMount:v,mounted:v,beforeUpdate:v,updated:v,beforeDestroy:v,beforeUnmount:v,destroyed:v,unmounted:v,activated:v,deactivated:v,errorCaptured:v,serverPrefetch:v,components:de,directives:de,watch:ar,provide:Lt,inject:lr};function Lt(e,t){return t?e?function(){return T(S(e)?e.call(this,this):e,S(t)?t.call(this,this):t)}:t:e}function lr(e,t){return de(zt(e),zt(t))}function zt(e){if(b(e)){const t={};for(let n=0;n<e.length;n++)t[e[n]]=e[n];return t}return e}function v(e,t){return e?[...new Set([].concat(e,t))]:t}function de(e,t){return e?T(Object.create(null),e,t):t}function Ut(e,t){return e?b(e)&&b(t)?[...new Set([...e,...t])]:T(Object.create(null),Kt(e),Kt(t??{})):t}function ar(e,t){if(!e)return t;if(!t)return e;const n=T(Object.create(null),e);for(const s in t)n[s]=v(e[s],t[s]);return n}let ur=null;function fr(e,t,n=!1){const s=Pe||fe;if(s||ur){const r=s?s.parent==null?s.vnode.appContext&&s.vnode.appContext.provides:s.parent.provides:void 0;if(r&&e in r)return r[e];if(arguments.length>1)return n&&S(t)?t.call(s&&s.proxy):t;g.NODE_ENV!=="production"&&O(`injection "${String(e)}" not found.`)}else g.NODE_ENV!=="production"&&O("inject() can only be used inside setup() or functional components.")}const pr={},Bt=e=>Object.getPrototypeOf(e)===pr,dr=wr,hr=Symbol.for("v-scx"),gr=()=>{{const e=fr(hr);return e||g.NODE_ENV!=="production"&&O("Server rendering context not provided. Make sure to only call useSSRContext() conditionally in the server build."),e}};function _r(e,t,n=$){const{immediate:s,deep:r,flush:i,once:o}=n;g.NODE_ENV!=="production"&&!t&&(s!==void 0&&O('watch() "immediate" option is only respected when using the watch(source, callback, options?) signature.'),r!==void 0&&O('watch() "deep" option is only respected when using the watch(source, callback, options?) signature.'),o!==void 0&&O('watch() "once" option is only respected when using the watch(source, callback, options?) signature.'));const c=T({},n);g.NODE_ENV!=="production"&&(c.onWarn=O);const a=t&&s||!t&&i!=="post";let f;if(ft){if(i==="sync"){const p=gr();f=p.__watcherHandles||(p.__watcherHandles=[])}else if(!a){const p=()=>{};return p.stop=se,p.resume=se,p.pause=se,p}}const d=Pe;c.call=(p,N,I)=>$t(p,d,N,I);let l=!1;i==="post"?c.scheduler=p=>{dr(p,d&&d.suspense)}:i!=="sync"&&(l=!0,c.scheduler=(p,N)=>{N?p():st(p)}),c.augmentJob=p=>{t&&(p.flags|=4),l&&(p.flags|=2,d&&(p.id=d.uid,p.i=d))};const u=Kn(e,t,c);return ft&&(f?f.push(u):a&&u()),u}function mr(e,t,n){const s=this.proxy,r=M(e)?e.includes(".")?br(s,e):()=>s[e]:e.bind(s,s);let i;S(t)?i=t:(i=t.handler,n=t);const o=$r(this),c=_r(r,i.bind(s),n);return o(),c}function br(e,t){const n=t.split(".");return()=>{let s=e;for(let r=0;r<n.length&&s;r++)s=s[n[r]];return s}}const Er=e=>e.__isSuspense;function wr(e,t){t&&t.pendingBranch?b(e)?t.effects.push(...e):t.effects.push(e):Mt(e)}const Jt=Symbol.for("v-fgt"),Nr=Symbol.for("v-txt"),Sr=Symbol.for("v-cmt"),Ce=[];let A=null;function Or(e=!1){Ce.push(A=e?null:[])}function xr(){Ce.pop(),A=Ce[Ce.length-1]||null}function vr(e){return e.dynamicChildren=A||en,xr(),A&&A.push(e),e}function yr(e,t,n,s,r,i){return vr(Yt(e,t,n,s,r,i,!0))}function Dr(e){return e?e.__v_isVNode===!0:!1}const Tr=(...e)=>Gt(...e),qt=({key:e})=>e??null,Ie=({ref:e,ref_key:t,ref_for:n})=>(typeof e=="number"&&(e=""+e),e!=null?M(e)||D(e)||S(e)?{i:fe,r:e,k:t,f:!!n}:e:null);function Yt(e,t=null,n=null,s=0,r=null,i=e===Jt?0:1,o=!1,c=!1){const a={__v_isVNode:!0,__v_skip:!0,type:e,props:t,key:t&&qt(t),ref:t&&Ie(t),scopeId:nr,slotScopeIds:null,children:n,component:null,suspense:null,ssContent:null,ssFallback:null,dirs:null,transition:null,el:null,anchor:null,target:null,targetStart:null,targetAnchor:null,staticCount:0,shapeFlag:i,patchFlag:s,dynamicProps:r,dynamicChildren:null,appContext:null,ctx:fe};return c?(at(a,n),i&128&&e.normalize(a)):n&&(a.shapeFlag|=M(n)?8:16),g.NODE_ENV!=="production"&&a.key!==a.key&&O("VNode created with invalid key (NaN). VNode type:",a.type),!o&&A&&(a.patchFlag>0||i&6)&&a.patchFlag!==32&&A.push(a),a}const Vr=g.NODE_ENV!=="production"?Tr:Gt;function Gt(e,t=null,n=null,s=0,r=null,i=!1){if((!e||e===sr)&&(g.NODE_ENV!=="production"&&!e&&O(`Invalid vnode type when creating vnode: ${e}.`),e=Sr),Dr(e)){const c=$e(e,t,!0);return n&&at(c,n),!i&&A&&(c.shapeFlag&6?A[A.indexOf(e)]=c:A.push(c)),c.patchFlag=-2,c}if(kt(e)&&(e=e.__vccOpts),t){t=Rr(t);let{class:c,style:a}=t;c&&!M(c)&&(t.class=Ke(c)),y(a)&&(Oe(a)&&!b(a)&&(a=T({},a)),t.style=We(a))}const o=M(e)?1:Er(e)?128:rr(e)?64:y(e)?4:S(e)?2:0;return g.NODE_ENV!=="production"&&o&4&&Oe(e)&&(e=h(e),O("Vue received a Component that was made a reactive object. This can lead to unnecessary performance overhead and should be avoided by marking the component with `markRaw` or using `shallowRef` instead of `ref`.",`
Component that was made reactive: `,e)),Yt(e,t,n,s,r,o,i,!0)}function Rr(e){return e?Oe(e)||Bt(e)?T({},e):e:null}function $e(e,t,n=!1,s=!1){const{props:r,ref:i,patchFlag:o,children:c,transition:a}=e,f=t?Ir(r||{},t):r,d={__v_isVNode:!0,__v_skip:!0,type:e.type,props:f,key:f&&qt(f),ref:t&&t.ref?n&&i?b(i)?i.concat(Ie(t)):[i,Ie(t)]:Ie(t):i,scopeId:e.scopeId,slotScopeIds:e.slotScopeIds,children:g.NODE_ENV!=="production"&&o===-1&&b(c)?c.map(Qt):c,target:e.target,targetStart:e.targetStart,targetAnchor:e.targetAnchor,staticCount:e.staticCount,shapeFlag:e.shapeFlag,patchFlag:t&&e.type!==Jt?o===-1?16:o|16:o,dynamicProps:e.dynamicProps,dynamicChildren:e.dynamicChildren,appContext:e.appContext,dirs:e.dirs,transition:a,component:e.component,suspense:e.suspense,ssContent:e.ssContent&&$e(e.ssContent),ssFallback:e.ssFallback&&$e(e.ssFallback),el:e.el,anchor:e.anchor,ctx:e.ctx,ce:e.ce};return a&&s&&Wt(d,a.clone(d)),d}function Qt(e){const t=$e(e);return b(e.children)&&(t.children=e.children.map(Qt)),t}function Cr(e=" ",t=0){return Vr(Nr,null,e,t)}function at(e,t){let n=0;const{shapeFlag:s}=e;if(t==null)t=null;else if(b(t))n=16;else if(typeof t=="object")if(s&65){const r=t.default;r&&(r._c&&(r._d=!1),at(e,r()),r._c&&(r._d=!0));return}else n=32,!t._&&!Bt(t)&&(t._ctx=fe);else S(t)?(t={default:t,_ctx:fe},n=32):(t=String(t),s&64?(n=16,t=[Cr(t)]):n=8);e.children=t,e.shapeFlag|=n}function Ir(...e){const t={};for(let n=0;n<e.length;n++){const s=e[n];for(const r in s)if(r==="class")t.class!==s.class&&(t.class=Ke([t.class,s.class]));else if(r==="style")t.style=We([t.style,s.style]);else if(tn(r)){const i=t[r],o=s[r];o&&i!==o&&!(b(i)&&i.includes(o))&&(t[r]=i?[].concat(i,o):o)}else r!==""&&(t[r]=s[r])}return t}let Pe=null,ut;{const e=_e(),t=(n,s)=>{let r;return(r=e[n])||(r=e[n]=[]),r.push(s),i=>{r.length>1?r.forEach(o=>o(i)):r[0](i)}};ut=t("__VUE_INSTANCE_SETTERS__",n=>Pe=n),t("__VUE_SSR_SETTERS__",n=>ft=n)}const $r=e=>{const t=Pe;return ut(e),e.scope.on(),()=>{e.scope.off(),ut(t)}};function Pr(e){return e.vnode.shapeFlag&4}let ft=!1;function Ar(e){return e.exposed?e.exposeProxy||(e.exposeProxy=new Proxy(Hn(Mn(e.exposed)),{get(t,n){if(n in t)return t[n];if(n in pe)return pe[n](e)},has(t,n){return n in t||n in pe}})):e.proxy}const Mr=/(?:^|[-_])(\w)/g,Fr=e=>e.replace(Mr,t=>t.toUpperCase()).replace(/[-_]/g,"");function Zt(e,t=!0){return S(e)?e.displayName||e.name:e.name||t&&e.__name}function Xt(e,t,n=!1){let s=Zt(t);if(!s&&t.__file){const r=t.__file.match(/([^/\\]+)\.\w+$/);r&&(s=r[1])}if(!s&&e&&e.parent){const r=i=>{for(const o in i)if(i[o]===t)return o};s=r(e.components||e.parent.type.components)||r(e.appContext.components)}return s?Fr(s):n?"App":"Anonymous"}function kt(e){return S(e)&&"__vccOpts"in e}function jr(){if(g.NODE_ENV==="production"||typeof window>"u")return;const e={style:"color:#3ba776"},t={style:"color:#1677ff"},n={style:"color:#f5222d"},s={style:"color:#eb2f96"},r={__vue_custom_formatter:!0,header(l){return y(l)?l.__isVue?["div",e,"VueInstance"]:D(l)?["div",{},["span",e,d(l)],"<",c("_value"in l?l._value:l),">"]:k(l)?["div",{},["span",e,V(l)?"ShallowReactive":"Reactive"],"<",c(l),`>${L(l)?" (readonly)":""}`]:L(l)?["div",{},["span",e,V(l)?"ShallowReadonly":"Readonly"],"<",c(l),">"]:null:null},hasBody(l){return l&&l.__isVue},body(l){if(l&&l.__isVue)return["div",{},...i(l.$)]}};function i(l){const u=[];l.type.props&&l.props&&u.push(o("props",h(l.props))),l.setupState!==$&&u.push(o("setup",l.setupState)),l.data!==$&&u.push(o("data",h(l.data)));const p=a(l,"computed");p&&u.push(o("computed",p));const N=a(l,"inject");return N&&u.push(o("injected",N)),u.push(["div",{},["span",{style:s.style+";opacity:0.66"},"$ (internal): "],["object",{object:l}]]),u}function o(l,u){return u=T({},u),Object.keys(u).length?["div",{style:"line-height:1.25em;margin-bottom:0.6em"},["div",{style:"color:#476582"},l],["div",{style:"padding-left:1.25em"},...Object.keys(u).map(p=>["div",{},["span",s,p+": "],c(u[p],!1)])]]:["span",{}]}function c(l,u=!0){return typeof l=="number"?["span",t,l]:typeof l=="string"?["span",n,JSON.stringify(l)]:typeof l=="boolean"?["span",s,l]:y(l)?["object",{object:u?h(l):l}]:["span",n,String(l)]}function a(l,u){const p=l.type;if(S(p))return;const N={};for(const I in l.ctx)f(p,I,u)&&(N[I]=l.ctx[I]);return N}function f(l,u,p){const N=l[p];if(b(N)&&N.includes(u)||y(N)&&u in N||l.extends&&f(l.extends,u,p)||l.mixins&&l.mixins.some(I=>f(I,u,p)))return!0}function d(l){return V(l)?"ShallowRef":l.effect?"ComputedRef":"Ref"}window.devtoolsFormatters?window.devtoolsFormatters.push(r):window.devtoolsFormatters=[r]}var Hr={};function Wr(){jr()}Hr.NODE_ENV!=="production"&&Wr();const Kr=(e,t)=>{const n=e.__vccOpts||e;for(const[s,r]of t)n[s]=r;return n},Ae=window.jQuery,Me=e=>e===void 0?e:Ae.fn.bootstrapTable.utils.extend(!0,Array.isArray(e)?[]:{},e),Lr={name:"BootstrapTable",props:{columns:{type:Array,require:!0},data:{type:[Array,Object],default(){}},options:{type:Object,default(){return{}}}},data(){return{optionsChangedIdx:0}},mounted(){this.$table=Ae(this.$el),this.$table.on("all.bs.table",(e,t,n)=>{let s=Ae.fn.bootstrapTable.events[t];s=s.replace(/([A-Z])/g,"-$1").toLowerCase(),this.$emit("on-all",...n),this.$emit(s,...n)}),this._initTable()},beforeUnmount(){this.$table.bootstrapTable("destroy")},methods:{_initTable(){const e={...Me(this.options),columns:Me(this.columns),data:Me(this.data)};this._hasInit?this.refreshOptions(e):(this.$table.bootstrapTable(e),this._hasInit=!0)},...(()=>{const e={};for(const t of Ae.fn.bootstrapTable.methods)e[t]=function(...n){return this.$table.bootstrapTable(t,...n)};return e})()},watch:{options:{handler(){this.optionsChangedIdx++},deep:!0},columns:{handler(){this.optionsChangedIdx++},deep:!0},optionsChangedIdx(){this._initTable()},data:{handler(){this.load(Me(this.data))},deep:!0}}};function zr(e,t,n,s,r,i){return Or(),yr("table")}return Kr(Lr,[["render",zr]])});
