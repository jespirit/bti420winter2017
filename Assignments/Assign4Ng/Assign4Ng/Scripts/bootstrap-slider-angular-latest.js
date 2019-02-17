(function(factory) {
    if (typeof define === 'function' && define.amd) {
        define(['bootstrap-slider', 'angular'], factory);
    } else if (typeof module === 'object' && module.exports) {
        module.exports = factory(require('bootstrap-slider'), require('angular'));
    } else if (window) {
        factory(window.Slider);
    }
})(function (Slider) {

angular.module('ui.bootstrap-slider', [])
    .directive('slider', ['$parse', '$timeout', '$rootScope', function ($parse, $timeout, $rootScope) {
        return {
            restrict: 'AE',
            replace: true,
            template: '<div><input class="slider-input" type="text" style="width:100%" /></div>',
            require: 'ngModel',
            scope: {
                max: "=",
                min: "=",
                step: "=",
                value: "=",
                ngModel: '=',
                ngDisabled: '=',
                range: '=',
                sliderid: '=',
                ticks: '=',
                ticksLabels: '=',
                ticksSnapBounds: '=',
                ticksPositions: '=',
                ticksTooltip: "=",
                scale: '=',
                focus: '=',
                rangeHighlights: '=',
                formatter: '&',
                onStartSlide: '&',
                onStopSlide: '&',
                onSlide: '&'
            },
            link: function ($scope, element, attrs, ngModelCtrl, $compile) {
                var ngModelDeregisterFn, ngDisabledDeregisterFn;

                var slider = initSlider();

                function initSlider() {
                    var options = {};

                    function setOption(key, value, defaultValue) {
                        options[key] = value || defaultValue;
                    }

                    function setFloatOption(key, value, defaultValue) {
                        options[key] = value || value === 0 ? parseFloat(value) : defaultValue;
                    }

                    function setBooleanOption(key, value, defaultValue) {
                        options[key] = value ? value + '' === 'true' : defaultValue;
                    }

                    function getArrayOrValue(value) {
                        return (angular.isString(value) && value.indexOf("[") === 0) ? angular.fromJson(value) : value;
                    }

                    /* Attributes that are not listed as part of the scope are
                     *  input bindings and not two-way bindings.
                     */
                    setOption('id', $scope.sliderid);
                    setOption('orientation', attrs.orientation, 'horizontal');
                    setOption('selection', attrs.selection, 'before');
                    setOption('handle', attrs.handle, 'round');
                    setOption('tooltip', attrs.sliderTooltip || attrs.tooltip, 'show');
                    setOption('tooltip_position', attrs.sliderTooltipPosition, 'top');
                    setOption('tooltipseparator', attrs.tooltipseparator, ':');
                    setOption('ticks', $scope.ticks);
                    setOption('ticks_labels', $scope.ticksLabels);
                    setOption('ticks_snap_bounds', $scope.ticksSnapBounds);
                    setOption('ticks_positions', $scope.ticksPositions);
                    setOption('ticks_tooltip', $scope.ticksTooltip, false);
                    setOption('rangeHighlights', $scope.rangeHighlights);
                    setOption('scale', $scope.scale, 'linear');
                    setOption('focus', $scope.focus);

                    setFloatOption('min', $scope.min, 0);
                    setFloatOption('max', $scope.max, 10);
                    setFloatOption('step', $scope.step, 1);
                    var strNbr = options.step + '';
                    var dotPos = strNbr.search(/[^.,]*$/);
                    var decimals = strNbr.substring(dotPos);
                    setFloatOption('precision', attrs.precision, decimals.length);

                    setBooleanOption('tooltip_split', attrs.tooltipsplit, false);
                    setBooleanOption('enabled', attrs.enabled, true);
                    setBooleanOption('naturalarrowkeys', attrs.naturalarrowkeys, false);
                    setBooleanOption('reversed', attrs.reversed, false);

                    setBooleanOption('range', $scope.range, false);
                    if (options.range) {
                        if (angular.isArray($scope.value)) {
                            options.value = $scope.value;
                        }
                        else if (angular.isString($scope.value)) {
                            options.value = getArrayOrValue($scope.value);
                            if (!angular.isArray(options.value)) {
                                var value = parseFloat($scope.value);
                                if (isNaN(value)) value = 5;

                                if (value < $scope.min) {
                                    value = $scope.min;
                                    options.value = [value, options.max];
                                }
                                else if (value > $scope.max) {
                                    value = $scope.max;
                                    options.value = [options.min, value];
                                }
                                else {
                                    options.value = [options.min, options.max];
                                }
                            }
                        }
                        else {
                            options.value = [options.min, options.max]; // This is needed, because of value defined at $.fn.slider.defaults - default value 5 prevents creating range slider
                        }
                        $scope.ngModel = options.value; // needed, otherwise turns value into [null, ##]
                    }
                    else {
                        setFloatOption('value', $scope.value, 5);
                    }

                    if (attrs.formatter) {
                        options.formatter = function(value) {
                            return $scope.formatter({value: value});
                        }
                    }

                    // Commit: https://github.com/jespirit/angular-bootstrap-slider/commit/c75d723daa4638a103934096f3243594cddd25dd
                    // git log -p --follow -G "picker" -- src/js/bootstrap-slider.js
                    // `picker` is legacy code removed in v4.0.0
                    // You need to go back to v3.0.0 to see the picker being used
                    // check if slider jQuery plugin exists
                    if (typeof window.$ !== 'undefined' && typeof $.fn === 'object' && $.fn.slider) {
                        // adding methods to jQuery slider plugin prototype
                        $.fn.slider.constructor.prototype.disable = function () {
                            /* FIXME: If `picker` is a jQuery object, then what does calling
                             * `off()` or `on()` with no arguments actually do?
                             */
                            this.picker.off();
                        };
                        $.fn.slider.constructor.prototype.enable = function () {
                            this.picker.on();
                        };
                    }

                    // destroy previous slider to reset all options
                    if (element[0].__slider)
                        element[0].__slider.destroy();

                    var slider = new Slider(element[0].getElementsByClassName('slider-input')[0], options);
                    element[0].__slider = slider;

                    // everything that needs slider element
                    // FIXME: if updateEvent is an array, then it will default to ['slide']
                    var updateEvent = getArrayOrValue(attrs.updateevent);
                    if (angular.isString(updateEvent)) {
                        // if only single event name in string
                        updateEvent = [updateEvent];
                    }
                    else {
                        // default to slide event
                        updateEvent = ['slide'];
                    }
                    angular.forEach(updateEvent, function (sliderEvent) {
                        slider.on(sliderEvent, function (ev) {
                            /* $setViewValue() should be called when you want to change the view value
                             * Note, however, it does not call `$render()` or change the control's DOM value.
                             * Note: This only seems to make sense when applying the `slider` directive
                             * as an attribute to a <div> or <span> which actually has a view value and not
                             * when the directive is applied as an element.
                             */
                            ngModelCtrl.$setViewValue(ev);
                        });
                    });
                    // Write data to the model via the ngModelController
                    slider.on('change', function (ev) {
                        ngModelCtrl.$setViewValue(ev.newValue);
                    });


                    // Event listeners
                    var sliderEvents = {
                        slideStart: 'onStartSlide',
                        slide: 'onSlide',
                        slideStop: 'onStopSlide'
                    };
                    // FIXME: should be attrs['onStartSlide'] and slider.on('slideStart', callback)
                    // angular.forEach(object|array, function(key, value, obj))
                    angular.forEach(sliderEvents, function (sliderEventAttr, sliderEvent) {
                        /* $parse(expression)
                         * @returns function(context, locals)
                         * <slider on-start-slide="status='started'"></slider>
                         * $parse("status='started'") -> fn(context) -> context.status = 'started'
                         */
                        var fn = $parse(attrs[sliderEventAttr]);
                        slider.on(sliderEvent, function (ev) {
                            // slide, slideStart, slideStop events pass the new slider value
                            if ($scope[sliderEventAttr]) {
                                $scope.$apply(function () {
                                    // The directive declares a new isolate scope and uses $parent
                                    // to reference the parent scope to execute the function.
                                    fn($scope.$parent, { $event: ev, value: ev });
                                });
                            }
                        });
                    });

                    // deregister ngDisabled watcher to prevent memory leaks
                    if (angular.isFunction(ngDisabledDeregisterFn)) {
                        ngDisabledDeregisterFn();
                        ngDisabledDeregisterFn = null;
                    }

                    ngDisabledDeregisterFn = $scope.$watch('ngDisabled', function (value) {
                        if (value) {
                            slider.disable();
                        }
                        else {
                            slider.enable();
                        }
                    });

                    // deregister ngModel watcher to prevent memory leaks
                    // Note: Just call the deregistration function returned by $watch()
                    // to deregister the $watcher
                    // Note: All watches will be removed when the scope is destroyed
                    if (angular.isFunction(ngModelDeregisterFn)) ngModelDeregisterFn();

                    /* $watch(watchExpression, listener, [objectEquality])
                     * listener: function(newVal, oldVal, scope)
                     * `scope` is the current scope
                     * @returns deregistration function for the listener
                     *  
                     * if `objectEquality` is true, then use angular.equals() for comparing
                     * versus strict compariosn via `!==` operator
                     */
                    ngModelDeregisterFn = $scope.$watch('ngModel', function (value) {
                        if($scope.range){
                            slider.setValue(value);
                        }else{
                            slider.setValue(parseFloat(value));
                        }
                        slider.relayout();
                    }, true);

                    return slider;
                }


                var watchers = ['min', 'max', 'step', 'range', 'scale', 'ticksLabels', 'ticks', 'rangeHighlights'];
                angular.forEach(watchers, function (prop) {
                    $scope.$watch(prop, function () {
                        slider = initSlider();
                    });
                });

                var globalEvents = ['relayout', 'refresh', 'resize'];
                angular.forEach(globalEvents, function(event) {
                    if(angular.isFunction(slider[event])) {
                        /* $on() is a method of $scope object
                           Events: slider:relayout, slider:refresh, slider:resize
                           Listen for these events and call the appropriate event handler.

                           Reference: https://stackoverflow.com/questions/28800426/what-is-on-in-angularjs
                         */
                        $scope.$on('slider:' + event, function () {
                            slider[event]();
                        });
                    }
                });
            }
        };
    }]);
});