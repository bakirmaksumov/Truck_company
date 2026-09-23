// Modern website animations and interactions
document.addEventListener('DOMContentLoaded', function () {
    const navToggle = document.querySelector('.nav-toggle');
    const nav = document.querySelector('.main-nav');
    const header = document.querySelector('.site-header');
    const navLinks = nav ? Array.from(nav.querySelectorAll('a')) : [];
    const backToTop = document.getElementById('backToTop');

    function closeMobileNav() {
        if (nav && window.innerWidth <= 980) {
            nav.classList.remove('open');
            nav.style.display = 'none';
        }
    }

    if (navToggle && nav) {
        navToggle.addEventListener('click', function () {
            const isOpen = nav.classList.toggle('open');
            nav.style.display = isOpen ? 'flex' : 'none';
            nav.style.flexDirection = 'column';
            nav.style.position = 'absolute';
            nav.style.top = '84px';
            nav.style.left = '16px';
            nav.style.right = '16px';
            nav.style.background = '#0b0d10';
            nav.style.padding = '16px';
            nav.style.borderRadius = '12px';
            nav.style.border = '1px solid rgba(246, 194, 27, 0.4)';
            nav.style.boxShadow = '0 16px 36px rgba(0, 0, 0, 0.5)';
            nav.style.zIndex = '30';
        });
    }

    navLinks.forEach(function (link) {
        link.addEventListener('click', closeMobileNav);
    });

    // Header scroll appearance & Back to top button
    function handleScroll() {
        const scrollY = window.scrollY || window.pageYOffset;
        if (header) {
            header.classList.toggle('is-scrolled', scrollY > 20);
        }
        if (backToTop) {
            backToTop.classList.toggle('visible', scrollY > 400);
        }
    }

    window.addEventListener('scroll', handleScroll, { passive: true });
    handleScroll();

    if (backToTop) {
        backToTop.addEventListener('click', function () {
            window.scrollTo({ top: 0, behavior: 'smooth' });
        });
    }

    // Scroll Reveal System using IntersectionObserver
    const revealTargets = document.querySelectorAll(
        '.reveal, .reveal-up, .reveal-down, .reveal-left, .reveal-right, .reveal-scale, ' +
        '.service-card, .feature-card, .stat-box, .about-copy, .about-photo, .quote-card, .company-card, .section-title, .section-lead'
    );

    if ('IntersectionObserver' in window) {
        const revealObserver = new IntersectionObserver(function (entries) {
            entries.forEach(function (entry) {
                if (entry.isIntersecting) {
                    entry.target.classList.add('revealed');
                    revealObserver.unobserve(entry.target);
                }
            });
        }, {
            threshold: 0.12,
            rootMargin: '0px 0px -40px 0px'
        });

        revealTargets.forEach(function (el) {
            if (!el.classList.contains('revealed')) {
                if (!el.classList.contains('reveal') &&
                    !el.classList.contains('reveal-up') &&
                    !el.classList.contains('reveal-left') &&
                    !el.classList.contains('reveal-right') &&
                    !el.classList.contains('reveal-scale')) {
                    el.classList.add('reveal');
                }
                revealObserver.observe(el);
            }
        });
    } else {
        revealTargets.forEach(function (el) {
            el.classList.add('revealed');
        });
    }

    // Animated Number Counter for Stats
    function animateStatNumbers() {
        const statElements = document.querySelectorAll('.stat-value');
        if (!statElements.length) return;

        const countObserver = new IntersectionObserver(function (entries) {
            entries.forEach(function (entry) {
                if (entry.isIntersecting) {
                    const el = entry.target;
                    const text = el.textContent.trim();
                    const match = text.match(/^([^0-9]*)([0-9]+(?:\.[0-9]+)?)(.*)$/);

                    if (match) {
                        const prefix = match[1] || '';
                        const targetNumber = parseFloat(match[2]);
                        const suffix = match[3] || '';
                        const isDecimal = match[2].includes('.');
                        const duration = 1800; // ms
                        const startTime = performance.now();

                        function updateNumber(currentTime) {
                            const elapsed = currentTime - startTime;
                            const progress = Math.min(elapsed / duration, 1);
                            // Ease out cubic
                            const easeProgress = 1 - Math.pow(1 - progress, 3);
                            const currentVal = targetNumber * easeProgress;

                            el.textContent = prefix + (isDecimal ? currentVal.toFixed(1) : Math.floor(currentVal).toLocaleString()) + suffix;

                            if (progress < 1) {
                                requestAnimationFrame(updateNumber);
                            } else {
                                el.textContent = text;
                            }
                        }

                        requestAnimationFrame(updateNumber);
                    }
                    countObserver.unobserve(el);
                }
            });
        }, { threshold: 0.4 });

        statElements.forEach(function (el) {
            countObserver.observe(el);
        });
    }

    if ('IntersectionObserver' in window) {
        animateStatNumbers();
    }

    // Active navigation section highlight
    const sections = Array.from(document.querySelectorAll('section[id]'));
    if (sections.length && navLinks.length && 'IntersectionObserver' in window) {
        const navObserver = new IntersectionObserver(function (entries) {
            entries.forEach(function (entry) {
                if (entry.isIntersecting) {
                    const id = entry.target.getAttribute('id');
                    navLinks.forEach(function (link) {
                        const href = link.getAttribute('href') || '';
                        const hashIndex = href.indexOf('#');
                        const linkHash = hashIndex >= 0 ? href.substring(hashIndex) : '';
                        link.classList.toggle('active', linkHash === '#' + id);
                    });
                }
            });
        }, { rootMargin: '-35% 0px -55% 0px' });

        sections.forEach(function (section) {
            navObserver.observe(section);
        });
    }

    // Auto-dismiss success toast
    const toast = document.getElementById('successToast');
    if (toast) {
        const closeBtn = toast.querySelector('.toast-close');
        if (closeBtn) {
            closeBtn.addEventListener('click', function () {
                toast.remove();
            });
        }
        setTimeout(function () {
            toast.style.transition = 'opacity 0.4s ease, transform 0.4s ease';
            toast.style.opacity = '0';
            toast.style.transform = 'translateX(30px)';
            setTimeout(function () { toast.remove(); }, 400);
        }, 5000);
    }
});

