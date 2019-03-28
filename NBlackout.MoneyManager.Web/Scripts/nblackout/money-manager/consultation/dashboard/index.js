var NBlackout;
(function(NBlackout) {
    var MoneyManager;
    (function(MoneyManager) {
        var Consultation;
        (function(Consultation) {
            var Dashboard;
            (function(Dashboard) {
                var Index;
                (function(Index) {
                    Index.accountsUrl = null;
                    Index.expenditureUrl = null;
                    Index.transactionsOfOrganizationUrl = null;

                    Index.init = function(options) {
                        this.accountsUrl = options.accountsUrl;
                        this.expenditureUrl = options.expenditureUrl;
                        this.transactionsOfOrganizationUrl = options.transactionsOfOrganizationUrl;
                    };
                })(Index = NBlackout.MoneyManager.Consultation.Dashboard.Index || (NBlackout.MoneyManager.Consultation.Dashboard.Index = {}));
            })(Dashboard = NBlackout.MoneyManager.Consultation.Dashboard || (NBlackout.MoneyManager.Consultation.Dashboard = {}));
        })(Consultation = NBlackout.MoneyManager.Consultation || (NBlackout.MoneyManager.Consultation = {}));
    })(MoneyManager = NBlackout.MoneyManager || (NBlackout.MoneyManager = {}));
})(NBlackout || (NBlackout = {}));

function searchByPropertyValue(array, property, value) {
    for (var i = 0; i < array.length; i++) {
        if (array[i][property] === value) {
            return i;
        }
    }

    return -1;
}

function buildAccountsChart(accounts) {
    var balancesByCategory = [];
    var balances = [];
    var total = 0;

    $.each(accounts, function(i, account) {
        var category = account.Category.TypeDisplayName;
        var balance = account.Balance;
        var color = Highcharts.getOptions().colors[balancesByCategory.length];

        var index = searchByPropertyValue(balancesByCategory, 'name', category);
        if (index === -1) {
            balancesByCategory.push({
                color: color,
                name: category,
                y: balance
            });
        } else {
            balancesByCategory[index].y += balance;

            color = balancesByCategory[index].color;
        }

        var id = account.Id;
        var title = account.Title;

        balances.push({
            color: color,
            id: id,
            name: title,
            y: balance
        });

        total += balance;
    });

    if (total !== 0) {
        $('#accountsChart').highcharts({
            legend: {
                margin: 45,
                verticalAlign: 'bottom'
            },
            series: [{
                borderWidth: 2,
                data: balancesByCategory,
                name: 'Solde total',
                point: {
                    events: {
                        legendItemClick: function() {
                            var category = this;
                            var data = this.series.chart.series[1].data;
                            var total = this.total;

                            $.each(data, function(i, point) {
                                if (point.color === category.color) {
                                    var visible = !point.visible;

                                    total += (visible) ? point.y : -point.y;
                                    point.setVisible(visible);
                                }
                            });

                            var text = 'Solde total : ' + total.toFixed(2) + '€';
                            $('#accountsChart').highcharts().setTitle(null, { text: text });
                        }
                    }
                },
                showInLegend: true,
                type: 'pie',
                visible: false,
            }, {
                allowPointSelect: true,
                borderWidth: 2,
                cursor: 'pointer',
                data: balances,
                dataLabels: {
                    formatter: function() {
                        return '<b>' + this.point.name + '</b><br>' + this.y.toFixed(2) + '€';
                    },
                    style: {
                        fontSize: 14,
                        fontWeight: 'normal'
                    }
                },
                name: 'Solde',
                point: {
                    events: {
                        select: function() {
                            var accountId = this.id;

                            NBlackout.Core.ajax({
                                url: NBlackout.MoneyManager.Consultation.Dashboard.Index.expenditureUrl,
                                data: {
                                    accountId: accountId
                                },
                                success: function(response) {
                                    buildExpenditureChart(accountId, response);
                                }
                            });
                        }
                    }
                },
                size: 225,
                states: {
                    hover: {
                        halo: {
                            opacity: 0.75
                        }
                    }
                },
                type: 'pie'
            }],
            subtitle: {
                text: 'Solde total : ' + total.toFixed(2) + '€'
            },
            title: {
                text: 'Synthèse des comptes'
            }
        });
    }
}

function buildExpenditureChart(accountId, response) {
    var transactionsByCategory = [];
    var expenditure = [];
    var total = 0;

    $.each(response, function(i, expense) {
        var organizationId = expense.OrganizationId;
        var organizationLabel = expense.OrganizationLabel;
        var categoryLabel = expense.CategoryLabel;
        var amount = Math.abs(expense.Amount);
        var color = Highcharts.getOptions().colors[transactionsByCategory.length];

        if (amount !== 0) {
            var index = searchByPropertyValue(transactionsByCategory, 'name', categoryLabel);
            if (index === -1) {
                transactionsByCategory.push({
                    color: color,
                    name: categoryLabel,
                    y: amount
                });
            } else {
                transactionsByCategory[index].y += amount;

                color = transactionsByCategory[index].color;
            }

            expenditure.push({
                color: color,
                id: organizationId,
                name: organizationLabel,
                y: amount
            });

            total += amount;
        }
    });

    if (total !== 0) {
        $('#expenditureChart').highcharts({
            legend: {
                align: 'left',
                layout: 'vertical',
                padding: 10,
                title: {
                    text: 'Modifier :'
                },
                verticalAlign: 'middle'
            },
            series: [{
                borderWidth: 2,
                data: transactionsByCategory,
                dataLabels: {
                    enabled: false
                },
                name: 'Montant total',
                point: {
                    events: {
                        legendItemClick: function() {
                            var category = this;
                            var data = this.series.chart.series[1].data;
                            var total = this.total;

                            $.each(data, function(i, point) {
                                if (point.color === category.color) {
                                    var visible = !point.visible;

                                    total += (visible) ? point.y : -point.y;
                                    point.setVisible(visible);
                                }
                            });

                            var text = 'Montant total : ' + total.toFixed(2) + '€';
                            $('#expenditureChart').highcharts().setTitle(null, { text: text });
                        }
                    }
                },
                showInLegend: true,
                size: 150,
                states: {
                    hover: {
                        enabled: false
                    }
                },
                type: 'pie'
            }, {
                allowPointSelect: true,
                borderWidth: 2,
                cursor: 'pointer',
                data: expenditure,
                dataLabels: {
                    formatter: function() {
                        return '<b>' + this.point.name + '</b><br>' + this.y.toFixed(2) + '€';
                    },
                    style: {
                        fontSize: 14,
                        fontWeight: 'normal'
                    }
                },
                innerSize: 175,
                name: 'Montant',
                point: {
                    events: {
                        select: function() {
                            var organizationId = this.id;
                            var organizationLabel = this.name;

                            NBlackout.Core.ajax({
                                url: NBlackout.MoneyManager.Consultation.Dashboard.Index.transactionsOfOrganizationUrl,
                                data: {
                                    accountId: accountId,
                                    organizationId: organizationId
                                },
                                success: function(response) {
                                    buildTransactionsByOrganizationChart(organizationLabel, response);
                                }
                            });
                        }
                    }
                },
                size: 225,
                states: {
                    hover: {
                        halo: {
                            opacity: 0.75
                        }
                    }
                },
                type: 'pie'
            }],
            subtitle: {
                text: 'Montant total : ' + total.toFixed(2) + '€'
            },
            title: {
                text: 'Dépenses par catégorie'
            }
        });
    }
}

function buildTransactionsByOrganizationChart(organizationLabel, response) {
    var transactionsByDate = [];
    var total = 0;

    $.each(response, function(i, transaction) {
        var date = transaction.Date;
        var amount = transaction.Amount;

        transactionsByDate.push([date, amount]);

        total += amount;
    });

    if (total !== 0) {
        $('#transactionsOfOrganizationChart').highcharts({
            chart: {
                events: {
                    load: function() {
                        var chart = this;
                        var extremes = chart.yAxis[0].getExtremes();

                        if (extremes.min === 0) {
                            chart.yAxis[0].setExtremes(-0.00001, extremes.max);
                        } else if (extremes.max === 0) {
                            chart.yAxis[0].setExtremes(extremes.min, 0.00001);
                        }
                    }
                }
            },
            legend: {
                enabled: false
            },
            plotOptions: {
                area: {
                    color: '#55BF3B',
                    fillColor: 'rgba(0, 191, 0, 0.4)',
                    negativeColor: '#F45B5B',
                    negativeFillColor: 'rgba(244, 0, 0, 0.4)'
                }
            },
            series: [{
                data: transactionsByDate,
                name: organizationLabel,
                type: 'area'
            }],
            subtitle: {
                text: 'Montant total : ' + total.toFixed(2) + '€'
            },
            title: {
                text: organizationLabel
            },
            xAxis: {
                title: {
                    text: 'Mois'
                },
                type: 'type'
            },
            yAxis: {
                lineWidth: 1,
                gridLineWidth: 0,
                minorGridLineWidth: 0,
                minorTickWidth: 1,
                tickWidth: 1,
                plotLines: [{
                    color: '#CCD6EB',
                    value: 0,
                    width: 1,
                    zIndex: 4
                }],
                title: {
                    text: 'Montant (€)'
                }
            }
        });
    }
}

function handleDashboardIndexEvents() {
    NBlackout.Core.ajax({
        url: NBlackout.MoneyManager.Consultation.Dashboard.Index.accountsUrl,
        success: function(response) {
            buildAccountsChart(response);
        }
    });
}