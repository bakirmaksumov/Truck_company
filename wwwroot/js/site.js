// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

document.addEventListener('DOMContentLoaded', function () {
    const navToggle = document.querySelector('.nav-toggle');
    const nav = document.querySelector('.main-nav');
    const header = document.querySelector('.site-header');
    const navLinks = nav ? Array.from(nav.querySelectorAll('a')) : [];

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
            nav.style.zIndex = '30';
        });
    }

    navLinks.forEach(function (link) {
        link.addEventListener('click', closeMobileNav);
    });

    // Header shadow on scroll
    if (header) {
        window.addEventListener('scroll', function () {
            if (window.scrollY > 12) {
                header.style.boxShadow = '0 10px 30px rgba(0, 0, 0, 0.35)';
            } else {
                header.style.boxShadow = 'none';
            }
        });
    }

    // Active section highlighting
    const sections = Array.from(document.querySelectorAll('section[id]'));
    if (sections.length && navLinks.length) {
        const observer = new IntersectionObserver(function (entries) {
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
        }, { rootMargin: '-45% 0px -50% 0px' });

        sections.forEach(function (section) {
            observer.observe(section);
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

    // Scroll-reveal using IntersectionObserver
    // Elements must carry a class: reveal, reveal-up, reveal-left, reveal-right, reveal-scale
    if ('IntersectionObserver' in window) {
        var revealSelectors = '.service-card, .feature-card, .pillar-card, .stat-box, .about-copy, .about-photo-wrap, ' +
            '.company-card, .quote-card, .section-title, .section-lead, ' +
            '.reveal, .reveal-up, .reveal-left, .reveal-right, .reveal-scale';

        var revealTargets = Array.from(document.querySelectorAll(revealSelectors));

        var revealObserver = new IntersectionObserver(function (entries) {
            entries.forEach(function (entry) {
                if (entry.isIntersecting) {
                    entry.target.classList.add('is-visible');
                    revealObserver.unobserve(entry.target);
                }
            });
        }, { threshold: 0.12, rootMargin: '0px 0px -40px 0px' });

        revealTargets.forEach(function (el) {
            // Only add reveal class if element doesn't already have one
            if (!el.classList.contains('reveal') &&
                !el.classList.contains('reveal-up') &&
                !el.classList.contains('reveal-left') &&
                !el.classList.contains('reveal-right') &&
                !el.classList.contains('reveal-scale')) {
                el.classList.add('reveal-up');
            }
            revealObserver.observe(el);
        });
    } else {
        // Fallback: make everything visible immediately if IntersectionObserver not supported
        document.querySelectorAll('.reveal, .reveal-up, .reveal-left, .reveal-right, .reveal-scale, ' +
            '.service-card, .feature-card, .stat-box').forEach(function (el) {
            el.classList.add('is-visible');
        });
    }
});

