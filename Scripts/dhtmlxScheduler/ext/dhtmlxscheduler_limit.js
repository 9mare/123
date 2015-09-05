/*
@license
dhtmlxScheduler.Net v.3.3.6 

This software is covered by DHTMLX Evaluation License. Contact sales@dhtmlx.com to get Commercial or Enterprise license. Usage without proper license is prohibited.

(c) Dinamenta, UAB.
*/
Scheduler.plugin(function(e){e.config.limit_start=null,e.config.limit_end=null,e.config.limit_view=!1,e.config.check_limits=!0,e.config.mark_now=!0,e.config.display_marked_timespans=!0,e._temp_limit_scope=function(){function t(t,i,a,n,r){var s=e,d=[],o={_props:"map_to",matrix:"y_property"};for(var _ in o){var l=o[_];if(s[_])for(var h in s[_]){var c=s[_][h],u=c[l];t[u]&&(d=s._add_timespan_zones(d,e._get_blocked_zones(i[h],t[u],a,n,r)))}}return d=s._add_timespan_zones(d,e._get_blocked_zones(i,"global",a,n,r));

}var i=null,a="dhx_time_block",n="default",r=function(e,t,i){return t instanceof Date&&i instanceof Date?(e.start_date=t,e.end_date=i):(e.days=t,e.zones=i),e},s=function(e,t,i){var n="object"==typeof e?e:{days:e};return n.type=a,n.css="",t&&(i&&(n.sections=i),n=r(n,e,t)),n};e.blockTime=function(t,i,a){var n=s(t,i,a);return e.addMarkedTimespan(n)},e.unblockTime=function(t,i,a){i=i||"fullday";var n=s(t,i,a);return e.deleteMarkedTimespan(n)},e.attachEvent("onBeforeViewChange",function(t,i,a,n){function r(t,i){
var a=e.config.limit_start,n=e.config.limit_end,r=e.date.add(t,1,i);return t.valueOf()>n.valueOf()||r<=a.valueOf()}return e.config.limit_view&&(n=n||i,a=a||t,r(n,a)&&i.valueOf()!=n.valueOf())?(setTimeout(function(){var t=r(i,a)?e.config.limit_start:i;e.setCurrentView(r(t,a)?null:t,a)},1),!1):!0}),e.checkInMarkedTimespan=function(i,a,r){a=a||n;for(var s=!0,d=new Date(i.start_date.valueOf()),o=e.date.add(d,1,"day"),_=e._marked_timespans;d<i.end_date;d=e.date.date_part(o),o=e.date.add(d,1,"day")){var l=+e.date.date_part(new Date(d)),h=d.getDay(),c=t(i,_,h,l,a);

if(c)for(var u=0;u<c.length;u+=2){var v=e._get_zone_minutes(d),g=i.end_date>o||i.end_date.getDate()!=d.getDate()?1440:e._get_zone_minutes(i.end_date),f=c[u],m=c[u+1];if(g>f&&m>v&&(s="function"==typeof r?r(i,v,g,f,m):!1,!s))break}}return!s};var d=e.checkLimitViolation=function(t){if(!t)return!0;if(!e.config.check_limits)return!0;var i=e,n=i.config,r=[];if(t.rec_type)for(var s=e.getRecDates(t),d=0;d<s.length;d++){var o=e._copy_event(t);e._lame_copy(o,s[d]),r.push(o)}else r=[t];for(var _=!0,l=0;l<r.length;l++){
var h=!0,o=r[l];o._timed=e.isOneDayEvent(o),h=n.limit_start&&n.limit_end?o.start_date.valueOf()>=n.limit_start.valueOf()&&o.end_date.valueOf()<=n.limit_end.valueOf():!0,h&&(h=!e.checkInMarkedTimespan(o,a,function(e,t,a,n,r){var s=!0;return r>=t&&t>=n&&((1440==r||r>a)&&(s=!1),e._timed&&i._drag_id&&"new-size"==i._drag_mode?(e.start_date.setHours(0),e.start_date.setMinutes(r)):s=!1),(a>=n&&r>a||n>t&&a>r)&&(e._timed&&i._drag_id&&"new-size"==i._drag_mode?(e.end_date.setHours(0),e.end_date.setMinutes(n)):s=!1),
s})),h||(h=i.checkEvent("onLimitViolation")?i.callEvent("onLimitViolation",[o.id,o]):h),_=_&&h}return _||(i._drag_id=null,i._drag_mode=null),_};e._get_blocked_zones=function(e,t,i,a,n){var r=[];if(e&&e[t])for(var s=e[t],d=this._get_relevant_blocked_zones(i,a,s,n),o=0;o<d.length;o++)r=this._add_timespan_zones(r,d[o].zones);return r},e._get_relevant_blocked_zones=function(e,t,i,a){var n=i[t]&&i[t][a]?i[t][a]:i[e]&&i[e][a]?i[e][a]:[];return n},e.attachEvent("onMouseDown",function(e){return!(e==a)}),
e.attachEvent("onBeforeDrag",function(t){return t?d(e.getEvent(t)):!0}),e.attachEvent("onClick",function(t,i){return d(e.getEvent(t))}),e.attachEvent("onBeforeLightbox",function(t){var a=e.getEvent(t);return i=[a.start_date,a.end_date],d(a)}),e.attachEvent("onEventSave",function(t,i,a){if(!i.start_date||!i.end_date){var n=e.getEvent(t);i.start_date=new Date(n.start_date),i.end_date=new Date(n.end_date)}if(i.rec_type){var r=e._lame_clone(i);return e._roll_back_dates(r),d(r)}return d(i)}),e.attachEvent("onEventAdded",function(t){
if(!t)return!0;var i=e.getEvent(t);return!d(i)&&e.config.limit_start&&e.config.limit_end&&(i.start_date<e.config.limit_start&&(i.start_date=new Date(e.config.limit_start)),i.start_date.valueOf()>=e.config.limit_end.valueOf()&&(i.start_date=this.date.add(e.config.limit_end,-1,"day")),i.end_date<e.config.limit_start&&(i.end_date=new Date(e.config.limit_start)),i.end_date.valueOf()>=e.config.limit_end.valueOf()&&(i.end_date=this.date.add(e.config.limit_end,-1,"day")),i.start_date.valueOf()>=i.end_date.valueOf()&&(i.end_date=this.date.add(i.start_date,this.config.event_duration||this.config.time_step,"minute")),
i._timed=this.isOneDayEvent(i)),!0}),e.attachEvent("onEventChanged",function(t){if(!t)return!0;var a=e.getEvent(t);if(!d(a)){if(!i)return!1;a.start_date=i[0],a.end_date=i[1],a._timed=this.isOneDayEvent(a)}return!0}),e.attachEvent("onBeforeEventChanged",function(e,t,i){return d(e)}),e.attachEvent("onBeforeEventCreated",function(t){var i=e.getActionData(t).date,a={_timed:!0,start_date:i,end_date:e.date.add(i,e.config.time_step,"minute")};return d(a)}),e.attachEvent("onViewChange",function(){e._mark_now();

}),e.attachEvent("onSchedulerResize",function(){return window.setTimeout(function(){e._mark_now()},1),!0}),e.attachEvent("onTemplatesReady",function(){e._mark_now_timer=window.setInterval(function(){e._is_initialized()&&e._mark_now()},6e4)}),e._mark_now=function(t){var i="dhx_now_time";this._els[i]||(this._els[i]=[]);var a=e._currentDate(),n=this.config;if(e._remove_mark_now(),!t&&n.mark_now&&a<this._max_date&&a>this._min_date&&a.getHours()>=n.first_hour&&a.getHours()<n.last_hour){var r=this.locate_holder_day(a);

this._els[i]=e._append_mark_now(r,a)}},e._append_mark_now=function(t,i){var a="dhx_now_time",n=e._get_zone_minutes(i),r={zones:[n,n+1],css:a,type:a};if(!this._table_view){if(this._props&&this._props[this._mode]){for(var s=this._props[this._mode],d=s.size||s.options.length,o=t,_=t+d,l=(this._els.dhx_cal_data[0].childNodes,[]),h=o;_>h;h++){var c=h;r.days=c;var u=e._render_marked_timespan(r,null,c)[0];l.push(u)}return l}return r.days=t,e._render_marked_timespan(r,null,t)}return"month"==this._mode?(r.days=+e.date.date_part(i),
e._render_marked_timespan(r,null,null)):void 0},e._remove_mark_now=function(){for(var e="dhx_now_time",t=this._els[e],i=0;i<t.length;i++){var a=t[i],n=a.parentNode;n&&n.removeChild(a)}this._els[e]=[]},e._marked_timespans={global:{}},e._get_zone_minutes=function(e){return 60*e.getHours()+e.getMinutes()},e._prepare_timespan_options=function(t){var i=[],a=[];if("fullweek"==t.days&&(t.days=[0,1,2,3,4,5,6]),t.days instanceof Array){for(var r=t.days.slice(),s=0;s<r.length;s++){var d=e._lame_clone(t);d.days=r[s],
i.push.apply(i,e._prepare_timespan_options(d))}return i}if(!t||!(t.start_date&&t.end_date&&t.end_date>t.start_date||void 0!==t.days&&t.zones))return i;var o=0,_=1440;"fullday"==t.zones&&(t.zones=[o,_]),t.zones&&t.invert_zones&&(t.zones=e.invertZones(t.zones)),t.id=e.uid(),t.css=t.css||"",t.type=t.type||n;var l=t.sections;if(l){for(var h in l)if(l.hasOwnProperty(h)){var c=l[h];c instanceof Array||(c=[c]);for(var s=0;s<c.length;s++){var u=e._lame_copy({},t);u.sections={},u.sections[h]=c[s],a.push(u);

}}}else a.push(t);for(var v=0;v<a.length;v++){var g=a[v],f=g.start_date,m=g.end_date;if(f&&m)for(var p=e.date.date_part(new Date(f)),x=e.date.add(p,1,"day");m>p;){var u=e._lame_copy({},g);delete u.start_date,delete u.end_date,u.days=p.valueOf();var y=f>p?e._get_zone_minutes(f):o,b=m>x||m.getDate()!=p.getDate()?_:e._get_zone_minutes(m);u.zones=[y,b],i.push(u),p=x,x=e.date.add(x,1,"day")}else g.days instanceof Date&&(g.days=e.date.date_part(g.days).valueOf()),g.zones=t.zones.slice(),i.push(g)}return i;

},e._get_dates_by_index=function(t,i,a){var n=[];i=e.date.date_part(new Date(i||e._min_date)),a=new Date(a||e._max_date);for(var r=i.getDay(),s=t-r>=0?t-r:7-i.getDay()+t,d=e.date.add(i,s,"day");a>d;d=e.date.add(d,1,"week"))n.push(d);return n},e._get_css_classes_by_config=function(e){var t=[];return e.type==a&&(t.push(a),e.css&&t.push(a+"_reset")),t.push("dhx_marked_timespan",e.css),t.join(" ")},e._get_block_by_config=function(e){var t=document.createElement("DIV");return e.html&&("string"==typeof e.html?t.innerHTML=e.html:t.appendChild(e.html)),
t},e._render_marked_timespan=function(t,i,a){var n=[],r=e.config,s=this._min_date,d=this._max_date,o=!1;if(!r.display_marked_timespans)return n;if(!a&&0!==a){if(t.days<7)a=t.days;else{var _=new Date(t.days);if(o=+_,!(+d>+_&&+_>=+s))return n;a=_.getDay()}var l=s.getDay();l>a?a=7-(l-a):a-=l}var h=t.zones,c=e._get_css_classes_by_config(t);if(e._table_view&&"month"==e._mode){var u=[],v=[];if(i)u.push(i),v.push(a);else{v=o?[o]:e._get_dates_by_index(a);for(var g=0;g<v.length;g++)u.push(this._scales[v[g]]);

}for(var g=0;g<u.length;g++){i=u[g],a=v[g];var f=Math.floor((this._correct_shift(a,1)-s.valueOf())/(864e5*this._cols.length)),m=this.locate_holder_day(a,!1)%this._cols.length;if(!this._ignores[m]){var p=e._get_block_by_config(t),x=Math.max(i.offsetHeight-1,0),y=Math.max(i.offsetWidth-1,0),b=this._colsS[m],w=this._colsS.heights[f]+(this._colsS.height?this.xy.month_scale_height+2:2)-1;p.className=c,p.style.top=w+"px",p.style.lineHeight=p.style.height=x+"px";for(var E=0;E<h.length;E+=2){var D=h[g],k=h[g+1];

if(D>=k)return[];var N=p.cloneNode(!0);N.style.left=b+Math.round(D/1440*y)+"px",N.style.width=Math.round((k-D)/1440*y)+"px",i.appendChild(N),n.push(N)}}}}else{var M=a;if(this._ignores[this.locate_holder_day(a,!1)])return n;if(this._props&&this._props[this._mode]&&t.sections&&t.sections[this._mode]){var L=this._props[this._mode];M=L.order[t.sections[this._mode]];var O=L.order[t.sections[this._mode]];if(L.days>1){var S=L.size||L.options.length;M=M*S+O}else M=O,L.size&&M>L.position+L.size&&(M=0)}i=i?i:e.locate_holder(M);

for(var g=0;g<h.length;g+=2){var D=Math.max(h[g],60*r.first_hour),k=Math.min(h[g+1],60*r.last_hour);if(D>=k){if(g+2<h.length)continue;return[]}var N=e._get_block_by_config(t);N.className=c;var T=24*this.config.hour_size_px+1,C=36e5;N.style.top=Math.round((60*D*1e3-this.config.first_hour*C)*this.config.hour_size_px/C)%T+"px",N.style.lineHeight=N.style.height=Math.max(Math.round(60*(k-D)*1e3*this.config.hour_size_px/C)%T,1)+"px",i.appendChild(N),n.push(N)}}return n},e._mark_timespans=function(){for(var t=this._els.dhx_cal_data[0],i=[],a=new Date(e._min_date),n=0,r=t.childNodes.length;r>n;n++){
var s=t.childNodes[n];s.firstChild&&(s.firstChild.className||"").indexOf("dhx_scale_hour")>-1||(i.push.apply(i,e._on_scale_add_marker(s,a)),a=e.date.add(a,1,"day"))}return i},e.markTimespan=function(t){var i=!1;this._els.dhx_cal_data||(e.get_elements(),i=!0);var a=e._marked_timespans_ids,n=e._marked_timespans_types,r=e._marked_timespans;e.deleteMarkedTimespan(),e.addMarkedTimespan(t);var s=e._mark_timespans();return i&&(e._els=[]),e._marked_timespans_ids=a,e._marked_timespans_types=n,e._marked_timespans=r,
s},e.unmarkTimespan=function(e){if(e)for(var t=0;t<e.length;t++){var i=e[t];i.parentNode&&i.parentNode.removeChild(i)}},e._marked_timespans_ids={},e.addMarkedTimespan=function(t){var i=e._prepare_timespan_options(t),a="global";if(i.length){var n=i[0].id,r=e._marked_timespans,s=e._marked_timespans_ids;s[n]||(s[n]=[]);for(var d=0;d<i.length;d++){var o=i[d],_=o.days,l=(o.zones,o.css,o.sections),h=o.type;if(o.id=n,l){for(var c in l)if(l.hasOwnProperty(c)){r[c]||(r[c]={});var u=l[c],v=r[c];v[u]||(v[u]={}),
v[u][_]||(v[u][_]={}),v[u][_][h]||(v[u][_][h]=[],e._marked_timespans_types||(e._marked_timespans_types={}),e._marked_timespans_types[h]||(e._marked_timespans_types[h]=!0));var g=v[u][_][h];o._array=g,g.push(o),s[n].push(o)}}else{r[a][_]||(r[a][_]={}),r[a][_][h]||(r[a][_][h]=[]),e._marked_timespans_types||(e._marked_timespans_types={}),e._marked_timespans_types[h]||(e._marked_timespans_types[h]=!0);var g=r[a][_][h];o._array=g,g.push(o),s[n].push(o)}}return n}},e._add_timespan_zones=function(e,t){var i=e.slice();

if(t=t.slice(),!i.length)return t;for(var a=0;a<i.length;a+=2)for(var n=i[a],r=i[a+1],s=a+2==i.length,d=0;d<t.length;d+=2){var o=t[d],_=t[d+1];if(_>r&&r>=o||n>o&&_>=n)i[a]=Math.min(n,o),i[a+1]=Math.max(r,_),a-=2;else{if(!s)continue;var l=n>o?0:2;i.splice(a+l,0,o,_)}t.splice(d--,2);break}return i},e._subtract_timespan_zones=function(e,t){for(var i=e.slice(),a=0;a<i.length;a+=2)for(var n=i[a],r=i[a+1],s=0;s<t.length;s+=2){var d=t[s],o=t[s+1];if(o>n&&r>d){var _=!1;n>=d&&o>=r&&i.splice(a,2),d>n&&(i.splice(a,2,n,d),
_=!0),r>o&&i.splice(_?a+2:a,_?0:2,o,r),a-=2;break}}return i},e.invertZones=function(t){return e._subtract_timespan_zones([0,1440],t.slice())},e._delete_marked_timespan_by_id=function(t){var i=e._marked_timespans_ids[t];if(i)for(var a=0;a<i.length;a++)for(var n=i[a],r=n._array,s=0;s<r.length;s++)if(r[s]==n){r.splice(s,1);break}},e._delete_marked_timespan_by_config=function(t){var i=e._marked_timespans,a=t.sections,r=t.days,s=t.type||n,d=[];if(a){for(var o in a)if(a.hasOwnProperty(o)&&i[o]){var _=a[o];

i[o][_]&&i[o][_][r]&&i[o][_][r][s]&&(d=i[o][_][r][s])}}else i.global[r]&&i.global[r][s]&&(d=i.global[r][s]);for(var l=0;l<d.length;l++){var h=d[l],c=e._subtract_timespan_zones(h.zones,t.zones);if(c.length)h.zones=c;else{d.splice(l,1),l--;for(var u=e._marked_timespans_ids[h.id],v=0;v<u.length;v++)if(u[v]==h){u.splice(v,1);break}}}for(var l in e._marked_timespans.timeline)for(var g in e._marked_timespans.timeline[l])for(var v in e._marked_timespans.timeline[l][g])v===s&&delete e._marked_timespans.timeline[l][g][v];

},e.deleteMarkedTimespan=function(t){if(arguments.length||(e._marked_timespans={global:{}},e._marked_timespans_ids={},e._marked_timespans_types={}),"object"!=typeof t)e._delete_marked_timespan_by_id(t);else{t.start_date&&t.end_date||(t.days||(t.days="fullweek"),t.zones||(t.zones="fullday"));var i=[];if(t.type)i.push(t.type);else for(var a in e._marked_timespans_types)i.push(a);for(var n=e._prepare_timespan_options(t),r=0;r<n.length;r++)for(var s=n[r],d=0;d<i.length;d++){var o=e._lame_clone(s);o.type=i[d],
e._delete_marked_timespan_by_config(o)}}},e._get_types_to_render=function(e,t){var i=e?e:{};for(var a in t||{})t.hasOwnProperty(a)&&(i[a]=t[a]);return i},e._get_configs_to_render=function(e){var t=[];for(var i in e)e.hasOwnProperty(i)&&t.push.apply(t,e[i]);return t},e._on_scale_add_marker=function(t,i){if(!e._table_view||"month"==e._mode){var a=i.getDay(),n=i.valueOf(),r=this._mode,s=e._marked_timespans,d=[],o=[];if(this._props&&this._props[r]){var _=this._props[r],l=_.options,h=e._get_unit_index(_,i),c=l[h];

if(_.days>1){var u=864e5,v=Math.floor((i-e._min_date)/u);i=e.date.add(e._min_date,Math.floor(v/l.length),"day"),i=e.date.date_part(i)}else i=e.date.date_part(new Date(this._date));if(a=i.getDay(),n=i.valueOf(),s[r]&&s[r][c.key]){var g=s[r][c.key],f=e._get_types_to_render(g[a],g[n]);d.push.apply(d,e._get_configs_to_render(f))}}var m=s.global,p=m[n]||m[a];d.push.apply(d,e._get_configs_to_render(p));for(var x=0;x<d.length;x++)o.push.apply(o,e._render_marked_timespan(d[x],t,i));return o}},e.attachEvent("onScaleAdd",function(){
e._on_scale_add_marker.apply(e,arguments)}),e.dblclick_dhx_marked_timespan=function(t,i){e.config.dblclick_create||e.callEvent("onScaleDblClick",[e.getActionData(t).date,i,t]),e.addEventNow(e.getActionData(t).date,null,t)}},e._temp_limit_scope()});